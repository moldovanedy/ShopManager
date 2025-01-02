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
            this.Close();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            ValueResult<Product> prodResult = ProductCache.SearchSingleProduct(this.ProductDropDown.Text);
            if (!prodResult.IsSuccess)
            {
                MessageBox.Show("Error");
                Logger.LogError(prodResult.ResultingError.Description);
                return;
            }

            if (!double.TryParse(this.QuantityTextBox.Text, out double quantity))
            {
                MessageBox.Show("Error on quantity");
                return;
            }

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
                    return;
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
                    return;
                }
            }

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
                //ERROR
                return;
            }

            ValueResult<Product> productResult = ProductCache.GetProduct(saleResult.Value.ProductID);
            if (!productResult.IsSuccess)
            {
                //ERROR
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
    }
}
