using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Model.DataModels;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    public partial class CreateSaleWindow : Form
    {
        /// <summary>
        /// Setting this to a value greater than 0 will turn this into a "modify" sale instead of "create" sale window.
        /// </summary>
        public long SaleIdToModify { get; set; } = -1;

        private bool _canUpdate = true;
        private bool _needsUpdate = false;
        private bool _isUpdatingFromDataSource = false;


        private bool _isCheckBoxUpdatedFromCode = false;
        private CheckState _previousCheckState = CheckState.Checked;

        public CreateSaleWindow()
        {
            InitializeComponent();

            this.DebounceTimer.Interval = 500;
            this.DebounceTimer.Tick += OnDebounceTimerTick;
        }

        ~CreateSaleWindow()
        {
            this.DebounceTimer.Tick -= OnDebounceTimerTick;
        }

        private void Translate()
        {
            this.ProductLabel.Text = Strings.Product;
            this.QuantityLabel.Text = Strings.Quantity;
            this.CancelButton.Text = Strings.Cancel;
            this.UpdateProdQuantityCheckBox.Text = Strings.Also_update_product_stock;

            if (SaleIdToModify >= 0)
            {
                this.Text = Strings.Modify_sale;
                this.TitleLabel.Text = Strings.Modify_an_existing_sale;
                this.CreateButton.Text = Strings.Modify_sale;
            }
            else
            {
                this.Text = Strings.Create_sale;
                this.TitleLabel.Text = Strings.Create_a_new_sale;
                this.CreateButton.Text = Strings.Create_sale;
            }
        }

        //when the user types in
        private void ProductDropDown_TextUpdate(object sender, EventArgs e)
        {
            _needsUpdate = true;
        }

        //when the user either types in or selects an item
        private void ProductDropDown_TextChanged(object sender, EventArgs e)
        {
            if (!_needsUpdate)
            {
                return;
            }

            if (_canUpdate)
            {
                _canUpdate = false;
                UpdateDropDown();
            }
            else
            {
                RestartDebounceTimer();
            }
        }

        //when the user selects an item
        private void ProductDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needsUpdate = false;
        }

        private void RestartDebounceTimer()
        {
            this.DebounceTimer.Stop();
            _canUpdate = false;
            this.DebounceTimer.Start();
        }

        private void OnDebounceTimerTick(object sender, EventArgs e)
        {
            _canUpdate = true;
            this.DebounceTimer.Stop();
            UpdateDropDown();
        }

        private void UpdateDropDown()
        {
            if (_isUpdatingFromDataSource)
            {
                return;
            }

            if (this.ProductDropDown.Text.Length > 1)
            {
                ValueResult<List<Product>> searchResult = ProductCache.SearchProducts(this.ProductDropDown.Text);
                HandleTextChanged(
                    searchResult.IsSuccess ?
                        searchResult.Value.Select((prod) => prod.Name).ToList() :
                        new List<string>());
            }
        }

        private void HandleTextChanged(List<string> dataSource)
        {
            string text = this.ProductDropDown.Text;

            int previousSelectionStart = this.ProductDropDown.SelectionStart;
            int previousSelectionLength = this.ProductDropDown.SelectionLength;

            if (dataSource.Count > 0)
            {
                //solves the issue of calling Update twice and overwriting everything here
                _isUpdatingFromDataSource = true;
                this.ProductDropDown.DataSource = dataSource;
                _isUpdatingFromDataSource = false;

                //select the autocompletion of the first item (best match)
                string bestMatchText = this.ProductDropDown.Items[0].ToString();
                this.ProductDropDown.SelectionStart =
                    Math.Min(text.Length, previousSelectionStart);
                this.ProductDropDown.SelectionLength =
                    Math.Max(bestMatchText.Length - text.Length, previousSelectionLength);
                this.ProductDropDown.DroppedDown = true;
                // Restore default cursor
                Cursor.Current = Cursors.Default;

                return;
            }
            else
            {
                this.ProductDropDown.DroppedDown = false;
                this.ProductDropDown.SelectionStart = text.Length;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            ValueResult<Product> prodResult = ProductCache.SearchSingleProduct(this.ProductDropDown.Text);
            if (!prodResult.IsSuccess)
            {
                MessageBox.Show("Error");
                Logger.LogError(prodResult.ResultingError.Description);
                goto Cleanup;
            }

            //handle differences between locales (decimal separator being "." or ","
            string quantityString =
                NumberGlobalizationHandler.GlobalizeNumericString(this.QuantityTextBox.Text);

            if (!double.TryParse(quantityString, out double quantity))
            {
                MessageBox.Show("Error on quantity");
                goto Cleanup;
            }

            //check before any modifications
            if (this.UpdateProdQuantityCheckBox.CheckState == CheckState.Checked && WillMakeNegativeStock())
            {
                ShowNegativeStockWarning(out DialogResult dialogResult);
                if (dialogResult == DialogResult.Cancel)
                {
                    goto Cleanup;
                }
            }

            ValueResult<Sale> previousSaleResult = SalesCache.GetSale(SaleIdToModify);
            Sale newObject = new Sale()
            {
                ProductID = prodResult.Value.ID,
                Quantity = quantity
            };

            if (SaleIdToModify < 0)
            {
                Result addResult = SalesCache.AddSale(newObject);

                if (!addResult.IsSuccess)
                {
                    MessageBox.Show("Error on store");
                    Logger.LogError(addResult.ResultingError.Description);
                    goto Cleanup;
                }
            }
            else
            {
                newObject.ID = SaleIdToModify;
                Result updateResult = SalesCache.UpdateSale(newObject);

                if (!updateResult.IsSuccess)
                {
                    MessageBox.Show("Error on update store");
                    Logger.LogError(updateResult.ResultingError.Description);
                    goto Cleanup;
                }
            }

            if (this.UpdateProdQuantityCheckBox.CheckState == CheckState.Checked)
            {
                //if it updates the sale, only consider the difference between the previous value and this value
                if (SaleIdToModify >= 0)
                {
                    if (!previousSaleResult.IsSuccess)
                    {
                        MessageBox.Show("Couldn't update the stock.");
                        Logger.LogError(previousSaleResult.ResultingError.Description);
                        goto Cleanup;
                    }

                    quantity -= previousSaleResult.Value.Quantity;
                }

                prodResult.Value.Quantity -= quantity;
                Result updateQuantityResult = ProductCache.UpdateProduct(prodResult.Value);
                if (!updateQuantityResult.IsSuccess)
                {
                    MessageBox.Show("Error on update quantity");
                    Logger.LogError(updateQuantityResult.ResultingError.Description);
                }
            }

        Cleanup:
            this.DialogResult = DialogResult.OK;
            MainForm.Instance.RefreshData();
            this.Close();
        }

        private void CreateSaleWindow_Shown(object sender, EventArgs e)
        {
            Translate();
            if (SaleIdToModify < 0)
            {
                return;
            }

            ValueResult<Sale> saleResult = SalesCache.GetSale(SaleIdToModify);
            if (!saleResult.IsSuccess)
            {
                Logger.LogError(saleResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ValueResult<Product> productResult = ProductCache.GetProduct(saleResult.Value.ProductID);
            if (!productResult.IsSuccess)
            {
                Logger.LogError(productResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.ProductDropDown.DataSource = new List<string>() { productResult.Value.Name };
            this.ProductDropDown.Text = productResult.Value.Name;
            this.QuantityTextBox.Text = saleResult.Value.Quantity.ToString();
        }

        private void QuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreateButton_Click(sender, e);
            }
        }

        private void UpdateProdQuantityCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (_isCheckBoxUpdatedFromCode)
            {
                return;
            }

            if (this.UpdateProdQuantityCheckBox.CheckState == CheckState.Checked && WillMakeNegativeStock())
            {
                ShowNegativeStockWarning(out _);
                return;
            }

            _previousCheckState = this.UpdateProdQuantityCheckBox.CheckState;
        }

        private void ShowNegativeStockWarning(out DialogResult dialogResult)
        {
            dialogResult = MessageBox.Show(
                    Messages.WARN_SALE_OUT_OF_STOCK_TEXT,
                    Messages.WARN_SALE_OUT_OF_STOCK_TITLE,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes)
            {
                _isCheckBoxUpdatedFromCode = true;
                this.UpdateProdQuantityCheckBox.CheckState = CheckState.Unchecked;
                _isCheckBoxUpdatedFromCode = false;
            }

            _previousCheckState = this.UpdateProdQuantityCheckBox.CheckState;
        }

        private bool WillMakeNegativeStock()
        {
            ValueResult<Product> prodResult = ProductCache.SearchSingleProduct(this.ProductDropDown.Text);
            if (!prodResult.IsSuccess)
            {
                Logger.LogError(prodResult.ResultingError.Description);
                return false;
            }

            string unverifiedValue = this.QuantityTextBox.Text;
            //handle differences between locales (decimal separator being "." or ","
            unverifiedValue = NumberGlobalizationHandler.GlobalizeNumericString(unverifiedValue);

            if (!double.TryParse(unverifiedValue, out double requestedQuantity))
            {
                return false;
            }

            //if it updates the sale, only consider the difference between the previous value and this value
            if (SaleIdToModify >= 0)
            {
                ValueResult<Sale> saleResult = SalesCache.GetSale(SaleIdToModify);
                if (!saleResult.IsSuccess)
                {
                    Logger.LogError(saleResult.ResultingError.Description);
                    return false;
                }

                requestedQuantity -= saleResult.Value.Quantity;
            }

            return prodResult.Value.Quantity - requestedQuantity < 0;
        }
    }
}
