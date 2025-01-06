using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Resources.Locale;

namespace ShopManager.Controllers
{
    internal static class SalesTableController
    {
        /// <summary>
        /// The key is the table's row index, the value is the product ID.
        /// </summary>
        private static readonly List<long> _rowToIDMapper = new List<long>() { 0 };
        private static bool _isDeletingMultipleRows = false;
        private static bool _hasAskedAboutStockUpdate = false;
        private static DialogResult _stockUpdateAnswer = DialogResult.Yes;

        /// <summary>
        /// Will be called when the user confirmed that it wants to delete the row.
        /// </summary>
        /// <param name="id">The ID of the row/record.</param>
        /// <returns></returns>
        internal static Result RecordDeletionRequested(DataGridViewRowCancelEventArgs e)
        {
            _isDeletingMultipleRows = MainForm.Instance.GetSalesTableUI().SelectedRows.Count > 1;
            if (!_isDeletingMultipleRows || !_hasAskedAboutStockUpdate)
            {
                _stockUpdateAnswer = MessageBox.Show(
                    Messages.UPDATE_STOCK_QUESTION_TEXT,
                    Messages.UPDATE_STOCK_QUESTION_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                _hasAskedAboutStockUpdate = true;
            }

            if (_stockUpdateAnswer == DialogResult.Yes)
            {
                ValueResult<Product> prodResult =
                    ProductCache.SearchSingleProduct(e.Row.Cells[1].Value.ToString());
                if (!prodResult.IsSuccess)
                {
                    MessageBox.Show("Error on stock update");
                    Logger.LogError(prodResult.ResultingError.Description);
                }

                double quantity = (double)e.Row.Cells[3].Value;
                prodResult.Value.Quantity += quantity;

                Result updateQuantityResult = ProductCache.UpdateProduct(prodResult.Value);
                if (!updateQuantityResult.IsSuccess)
                {
                    MessageBox.Show("Error on stock update");
                    Logger.LogError(updateQuantityResult.ResultingError.Description);
                }
            }

            Result uiDeletionResult = SalesCache.DeleteSale(_rowToIDMapper[e.Row.Index]);
            _rowToIDMapper.RemoveAt(e.Row.Index);
            return uiDeletionResult;
        }

        internal static void SelectionChanged()
        {
            //because selection will change when a row is deleted
            if (!_isDeletingMultipleRows)
            {
                _hasAskedAboutStockUpdate = false;
            }
        }

        internal static void RowAdded()
        {
            _rowToIDMapper.Add(0);
        }

        internal static void CellValueRequested(DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex == MainForm.Instance.GetSalesTableUI().Rows.Count - 1)
            {
                return;
            }

            ValueResult<Sale> valueResult = SalesCache.GetSale(_rowToIDMapper[e.RowIndex]);
            if (!valueResult.IsSuccess)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Value = valueResult.Value.ID;
                return;
            }

            e.Value = GetSaleProperty(valueResult.Value, e.ColumnIndex);
        }

        internal static void RepopulateTable()
        {
            DataGridView uiTable = MainForm.Instance.GetSalesTableUI();
            List<Sale> sales = SalesCache.GetAllSalesFromCurrentPage();

            //TODO: implement more sorting options (default is ID ascending)
            sales.Sort((a, b) =>
            {
                return a.ID - b.ID >= 0 ? 1 : -1;
            });

            uiTable.Rows.Clear();

            for (int i = 0; i < sales.Count; i++)
            {
                if (i >= _rowToIDMapper.Count)
                {
                    _rowToIDMapper.Add(0);
                }
                _rowToIDMapper[i] = sales[i].ID;

                //dummy values that will be overwritten
                uiTable.Rows[i].Cells[0].Value = "???";
                uiTable.Rows[i].Cells[1].Value = "???";
                uiTable.Rows[i].Cells[2].Value = "???";
                uiTable.Rows[i].Cells[3].Value = "0";
                uiTable.Rows[i].Cells[4].Value = "0";

                uiTable.Rows.Add();
            }

            uiTable.Refresh();
        }

        private static object GetSaleProperty(Sale sale, int columnIndex)
        {
            if (columnIndex < 1 || columnIndex > 4)
            {
                return null;
            }

            ValueResult<Product> prodResult = ProductCache.GetProduct(sale.ProductID);
            if (!prodResult.IsSuccess)
            {
                if (columnIndex == 1)
                {
                    return "???";
                }
                else
                {
                    return 0;
                }
            }

            switch (columnIndex)
            {
                case 1:
                    return prodResult.Value.Name;
                case 2:
                    ValueResult<ProductCategory> categoryResult =
                        CategoriesCache.GetCategory(prodResult.Value.CategoryID);
                    return categoryResult.IsSuccess ? categoryResult.Value.Name : "???";
                case 3:
                    return sale.Quantity;
                case 4:
                    return Math.Round(prodResult.Value.Price * sale.Quantity, 2);
                default:
                    return null;
            }
        }
    }
}
