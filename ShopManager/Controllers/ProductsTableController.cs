using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Controller.Validation;
using ShopManager.Model.DataModels;
using ShopManager.Resources.Locale;

namespace ShopManager.Controllers
{
    internal static class ProductsTableController
    {
        /// <summary>
        /// This will be set only when the table data is refreshed, as this action shouldn't update the products.
        /// </summary>
        internal static bool IsAddingFromCode { get; set; } = false;

        /// <summary>
        /// The key is the table's row index, the value is the product ID.
        /// </summary>
        private static readonly List<long> _rowToIDMapper = new List<long>() { 0 };

        /// <summary>
        /// Will be called when the user confirmed that it wants to delete the row.
        /// </summary>
        /// <param name="id">The ID of the row/record.</param>
        /// <returns></returns>
        internal static Result RecordDeletionRequested(DataGridViewRowCancelEventArgs e)
        {
            //dependent sales
            if (SalesCache.GetAllSalesFromCurrentPage()
                .Where((sale) => sale.ProductID == _rowToIDMapper[e.Row.Index])
                .Any())
            {
                ValueResult<Product> productResult = ProductCache.GetProduct(_rowToIDMapper[e.Row.Index]);

                MessageBox.Show(
                    string.Format(
                        Messages.DEPENDENT_SALES_ERROR_TEXT,
                        productResult.IsSuccess ? productResult.Value.Name : ""),
                    Messages.DEPENDENT_SALES_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                e.Cancel = true;
                return Result.Failed(new Error());
            }

            Result uiDeletionResult = ProductCache.DeleteProduct(_rowToIDMapper[e.Row.Index]);
            _rowToIDMapper.RemoveAt(e.Row.Index);
            return uiDeletionResult;
        }

        internal static void RowAdded()
        {
            _rowToIDMapper.Add(0);
        }

        internal static Result CellValueSubmitted(DataGridViewCellValueEventArgs e)
        {
            if (IsAddingFromCode)
            {
                return Result.Successful();
            }
            if (e.Value == null)
            {
                e.Value = "";
            }

            Result actionResult;
            ValueResult<Product> existingProduct = ProductCache.GetProduct(_rowToIDMapper[e.RowIndex]);

            //it means "update"
            if (existingProduct.IsSuccess)
            {
                actionResult = ValidateRawValue(e.Value.ToString(), e.ColumnIndex, out object correctValue);
                if (!actionResult.IsSuccess)
                {
                    return actionResult;
                }

                ModifyProductProperty(existingProduct.Value, e.ColumnIndex, correctValue);
                actionResult = ProductCache.UpdateProduct(existingProduct.Value, true);
            }
            //it means "add"
            else
            {
                Product newProduct = new Product()
                {
                    //this MUST exist, no check for error here
                    CategoryID = CategoriesCache.SearchSingleCategory("").Value.ID
                };
                actionResult = ProductCache.AddProduct(newProduct, true);
                _rowToIDMapper[e.RowIndex] = newProduct.ID;

                actionResult = ValidateRawValue(e.Value.ToString(), e.ColumnIndex, out object correctValue);
                if (!actionResult.IsSuccess)
                {
                    return actionResult;
                }

                ModifyProductProperty(newProduct, e.ColumnIndex, correctValue);
                //this is to request the default data for this newly added product so they show up
                MainForm.Instance.GetProductsTableUI().Invalidate();
            }

            return actionResult;
        }

        internal static void CellValueRequested(DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex == MainForm.Instance.GetProductsTableUI().Rows.Count - 1)
            {
                return;
            }

            ValueResult<Product> valueResult = ProductCache.GetProduct(_rowToIDMapper[e.RowIndex]);
            if (!valueResult.IsSuccess)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Value = valueResult.Value.ID;
            }
            else
            {
                e.Value = GetProductProperty(valueResult.Value, e.ColumnIndex);
            }
        }

        internal static void RepopulateTable()
        {
            DataGridView uiTable = MainForm.Instance.GetProductsTableUI();
            List<Product> products = ProductCache.GetAllProductsFromCurrentPage();

            //TODO: implement more sorting options (default is ID ascending)
            products.Sort((a, b) =>
            {
                return a.ID - b.ID >= 0 ? 1 : -1;
            });

            uiTable.Rows.Clear();

            //set the combo box values
            ((DataGridViewComboBoxColumn)uiTable.Columns[8]).Items.Clear();
            foreach (ProductCategory category in CategoriesCache.GetAllCategories())
            {
                ((DataGridViewComboBoxColumn)uiTable.Columns[8]).Items.Add(category.Name);
            }

            IsAddingFromCode = true;
            for (int i = 0; i < products.Count; i++)
            {
                if (i >= _rowToIDMapper.Count)
                {
                    _rowToIDMapper.Add(0);
                }
                _rowToIDMapper[i] = products[i].ID;

                uiTable.Rows[i].Cells[0].Value = products[i].ID;
                uiTable.Rows[i].Cells[1].Value = products[i].Name;
                uiTable.Rows[i].Cells[2].Value = products[i].Description;
                uiTable.Rows[i].Cells[3].Value = products[i].Price;
                uiTable.Rows[i].Cells[4].Value = products[i].PricePerKg;
                uiTable.Rows[i].Cells[5].Value = products[i].PurchaseDate;
                uiTable.Rows[i].Cells[6].Value = products[i].ExpiryDate;
                uiTable.Rows[i].Cells[7].Value = products[i].Quantity;

                ValueResult<ProductCategory> categoryResult =
                    CategoriesCache.GetCategory(products[i].CategoryID);
                uiTable.Rows[i].Cells[8].Value = categoryResult.IsSuccess ? categoryResult.Value.Name : "???";

                uiTable.Rows.Add();
            }
            IsAddingFromCode = false;

            uiTable.Refresh();
        }


        private static Result ValidateRawValue(string value, int columnIndex, out object correctValue)
        {
            correctValue = null;
            ValueResult<string> validationResult = null;

            if (columnIndex < 1 || columnIndex > 8)
            {
                return Result.Failed(new Error());
            }

            switch (columnIndex)
            {
                case 1:
                    validationResult = ProductValidations.ValidateName(value);
                    correctValue = validationResult.Value;
                    break;
                case 2:
                    validationResult = ProductValidations.ValidateDescription(value);
                    correctValue = validationResult.Value;
                    break;
                case 3:
                case 4:
                    validationResult = ProductValidations.ValidatePrice(value, out double price);
                    correctValue = price;
                    break;
                case 5:
                case 6:
                    validationResult = ProductValidations.ValidateDate(value, out DateTime dateTime);
                    correctValue = dateTime;
                    break;
                case 7:
                    validationResult = ProductValidations.ValidateQuantity(value, out double quantity);
                    correctValue = quantity;
                    break;
                case 8:
                    ValueResult<ProductCategory> categoryResult =
                        CategoriesCache.SearchSingleCategory(value);
                    if (categoryResult.IsSuccess)
                    {
                        validationResult = ValueResult<string>.Successful(categoryResult.Value.Name);
                        correctValue = categoryResult.Value.Name;
                    }
                    else
                    {
                        validationResult = ValueResult<string>.Failed(new Error());
                        correctValue = "???";
                    }
                    break;
                default:
                    Logger.LogError("UNREACHABLE: columnIndex out of expected range");
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }

            if (validationResult != null && validationResult.IsSuccess)
            {
                return Result.Successful();
            }
            else
            {
                return Result.Failed(validationResult.ResultingError);
            }
        }

        private static bool ModifyProductProperty(Product product, int columnIndex, object newValue)
        {
            if (columnIndex < 1 || columnIndex > 8)
            {
                return false;
            }

            try
            {
                switch (columnIndex)
                {
                    case 1:
                        product.Name = (string)newValue;
                        break;
                    case 2:
                        product.Description = (string)newValue;
                        break;
                    case 3:
                        product.Price = (double)newValue;
                        break;
                    case 4:
                        product.PricePerKg = (double)newValue;
                        break;
                    case 5:
                        product.PurchaseDate = (DateTime)newValue;
                        break;
                    case 6:
                        product.ExpiryDate = (DateTime)newValue;
                        break;
                    case 7:
                        product.Quantity = (double)newValue;
                        break;
                    case 8:
                        ValueResult<ProductCategory> categoryResult =
                            CategoriesCache.SearchSingleCategory((string)newValue);
                        product.CategoryID =
                            categoryResult.IsSuccess ?
                                (long)categoryResult.Value.ID :
                                throw new ApplicationException($"Invalid category name: {(string)newValue}");
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static object GetProductProperty(Product product, int columnIndex)
        {
            if (columnIndex < 1 || columnIndex > 8)
            {
                return null;
            }

            switch (columnIndex)
            {
                case 1:
                    return product.Name;
                case 2:
                    return product.Description;
                case 3:
                    return product.Price;
                case 4:
                    return product.PricePerKg;
                case 5:
                    return product.PurchaseDate;
                case 6:
                    return product.ExpiryDate;
                case 7:
                    return product.Quantity;
                case 8:
                    ValueResult<ProductCategory> categoryResult =
                        CategoriesCache.GetCategory(product.CategoryID);
                    return categoryResult.IsSuccess ? categoryResult.Value.Name : "???";
                default:
                    return null;
            }
        }
    }
}
