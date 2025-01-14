using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ShopManager.AccountManagement;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Controller.XmlDataManager;
using ShopManager.Controllers;
using ShopManager.Extensions;
using ShopManager.Model.DataModels;
using ShopManager.Properties;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    public partial class MainForm : Form, IUserForm
    {
        public static MainForm Instance { get; private set; }

        public static bool PendingSavesExist { get; private set; } = false;

        private readonly Color NormalColor = Color.FromArgb(0xff, 0x42, 0x42, 0x42);
        private readonly Color LighterColor1 = Color.FromArgb(0xff, 0x51, 0x51, 0x51);

        private bool _isSearchRelevant = false;

        public MainForm()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                this.Close();
                return;
            }

            InitializeComponent();
            Settings settings = Settings.Default;
            switch (settings.Language)
            {
                case "ro":
                    RoLangMenuItem_Click(null, null);
                    break;
                case "en":
                default:
                    EnLangMenuItem_Click(null, null);
                    break;
            }

            Translate();
            this.AppMenuBar.Renderer = new CustomMenuBarRenderer();
            this.AppMenuBar.ForeColor = Color.White;

            this.Tools.BackColor = NormalColor;
            this.Tools.ForeColor = Color.White;

            this.StatusBar.BackColor = LighterColor1;
            this.StatusBar.ForeColor = Color.White;

            //this is supposed to change the hover color, but it doesn't work
            foreach (ToolStripItem tool in this.Tools.Items)
            {
                if (tool is ToolStripButton toolButton)
                {
                    toolButton.MouseEnter += this.ToolButtonMouseEnter;
                    toolButton.MouseLeave += this.ToolButtonMouseLeave;
                }
            }

            SetupMenu(this.FileMenuItem);
            SetupMenu(this.HelpMenuItem);
            SetupMenu(this.ExportMenuItem);
        }

        public void Translate()
        {
            TogglePendingSaveVisibility(false);

            //top-level
            this.NumberOfProductsLabel.Text = Strings.Number_of_products;
            this.CreateSaleButton.Text = Strings.Create_sale;
            this.AccountButton.Text = Strings.Account;
            this.TabControl.TabPages[0].Text = Strings.Products;
            this.TabControl.TabPages[1].Text = Strings.Sales;
            this.TabControl.TabPages[2].Text = Strings.Product_categories;

            //menu bar
            this.FileMenuItem.Text = Strings.File;
            this.LanugageMenuItem.Text = Strings.Language;
            this.ExitMenuItem.Text = Strings.Exit;

            this.ExportMenuItem.Text = Strings.Export;
            this.ExportSalesMenuItem.Text = Strings.Sales;
            this.ExportProductsMenuItem.Text = Strings.Products;

            this.HelpMenuItem.Text = Strings.Help;
            this.AboutMenuItem.Text = Strings.About;

            //table headers
            this.ProductsTable.Columns[1].HeaderText = Strings.Name;
            this.ProductsTable.Columns[2].HeaderText = Strings.Description;
            this.ProductsTable.Columns[3].HeaderText = Strings.Price;
            this.ProductsTable.Columns[4].HeaderText = Strings.Price_per_KG;
            this.ProductsTable.Columns[5].HeaderText = Strings.Purchase_date;
            this.ProductsTable.Columns[6].HeaderText = Strings.Expiry_date;
            this.ProductsTable.Columns[7].HeaderText = Strings.Quantity;
            this.ProductsTable.Columns[8].HeaderText = Strings.Category;

            this.SalesTable.Columns[1].HeaderText = Strings.Product;
            this.SalesTable.Columns[2].HeaderText = Strings.Product_category;
            this.SalesTable.Columns[3].HeaderText = Strings.Quantity;

            //tools
            this.SaveButton.Text = Strings.Save;
            this.SaveButton.ToolTipText = Strings.Save_changes;
            this.DiscardChangesButton.Text = Strings.Discard_changes;
            this.DiscardChangesButton.ToolTipText = Strings.Discard_all_the_changes_made_from_the_last_save;

            //categories
            this.DeleteCategoriesButton.Text = Strings.Delete_selected;
            this.DeselectButton.Text = Strings.Deselect;
            //this is just to trigger the SelectedIndexChanged, that in turn will translate some controls
            this.CategoriesListBox.Items.Add("A");
            this.CategoriesListBox.SelectedIndex = 0;
            this.CategoriesListBox.SelectedItems.Clear();
            RepopulateCategoriesListBox();
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            //await MasterDBManager.InitializeDBAsync();

            Task[] loadTasks = new Task[4];
            loadTasks[0] = ProductCache.RegenerateCacheFromDBAsync();
            loadTasks[1] = SalesCache.RegenerateCacheFromDBAsync();
            loadTasks[2] = CategoriesCache.RegenerateCacheFromDBAsync();
            loadTasks[3] = UsersCache.RegenerateCacheFromDBAsync();

            BasicAccountControlWindow loginWindow = new BasicAccountControlWindow();
            DialogResult dialogResult = loginWindow.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Close();
                return;
            }

            await Task.WhenAll(loadTasks);
            RefreshData();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PendingSavesExist)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                Messages.SAVE_CHANGES_QUESTION_TEXT,
                Messages.SAVE_CHANGES_QUESTION_TITLE,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                e.Cancel = true;
                await SaveChangesAsync();
                this.Close();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        internal DataGridView GetProductsTableUI()
        {
            return this.ProductsTable;
        }

        internal DataGridView GetSalesTableUI()
        {
            return this.SalesTable;
        }

        internal ToolStripTextBox GetSearchBar()
        {
            return this.SearchBar;
        }

        internal void RefreshData()
        {
            ProductsTableController.RepopulateTable();
            SalesTableController.RepopulateTable();
            RepopulateCategoriesListBox();
        }

        internal void TogglePendingSaveVisibility(bool pendingSavesExist)
        {
            PendingSavesExist = pendingSavesExist;
            Text = Strings.Shop_manager + (pendingSavesExist ? " *" : "");
        }


        #region Dynamically added
        private void ToolButtonMouseEnter(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackColor = LighterColor1;
        }

        private void ToolButtonMouseLeave(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackColor = NormalColor;
        }
        #endregion

        /// <summary>
        /// Sets the text color to white and translations for a whole top-level menu recursively.
        /// </summary>
        /// <param name="higherLevelMenu"></param>
        private void SetupMenu(ToolStripMenuItem higherLevelMenu)
        {
            foreach (object subMenu in higherLevelMenu.DropDownItems)
            {
                if (!(subMenu is ToolStripMenuItem menu))
                {
                    continue;
                }

                menu.ForeColor = Color.White;
                menu.Text = Strings.ResourceManager.GetString(menu.Text) ?? menu.Text;
                SetupMenu(menu);
            }
        }

        #region Products table
        private void ProductsTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ProductsTableController.RowAdded();
        }

        private void ProductsTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Result deleteResult = ProductsTableController.RecordDeletionRequested(e);
            TogglePendingSaveVisibility(PendingSavesExist || deleteResult.IsSuccess);

            if (!deleteResult.IsSuccess)
            {
                Logger.LogError(deleteResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ProductsTable_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            Result updateResult = ProductsTableController.CellValueSubmitted(e);
            if (!updateResult.IsSuccess)
            {
                Logger.LogError(updateResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!ProductsTableController.IsAddingFromCode)
            {
                TogglePendingSaveVisibility(true);
            }
        }

        private void ProductsTable_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            ProductsTableController.CellValueRequested(e);
        }

        private void ProductsTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //only the 5th and 6th columns are date pickers
            if (e.ColumnIndex != 5 && e.ColumnIndex != 6)
            {
                return;
            }

            e.Cancel = true;
            DateTimePicker pickerDialog = new DateTimePicker
            {
                DateTimeValue =
                    (DateTime)
                        (this.ProductsTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ??
                        DateTime.MinValue)
            };

            DialogResult result = pickerDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            TogglePendingSaveVisibility(true);

            //if it is the final empty row, add a new one to force refresh and let further editing
            if (e.RowIndex == this.ProductsTable.Rows.Count - 1)
            {
                this.ProductsTable.Rows.Add();
                ProductsTable_UserAddedRow(this, null);
            }

            //set the date
            ProductsTable_CellValuePushed(
                this,
                new DataGridViewCellValueEventArgs(e.ColumnIndex, e.RowIndex)
                {
                    Value = pickerDialog.DateTimeValue
                });
        }

        private void ProductsTable_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
        {
            e.Response = this.ProductsTable.IsCurrentCellDirty;
        }

        //this forces any change in the drop down to fire a CellValueSubmitted event
        private void ProductsTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.ProductsTable.CurrentCell.ColumnIndex != 8)
            {
                return;
            }

            this.ProductsTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ProductsTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logger.LogError(e.Exception.Message);

            MessageBox.Show(
                Messages.UNEXPECTED_ERROR_TEXT,
                Messages.UNEXPECTED_ERROR_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            e.Cancel = true;
        }

        private void ProductsTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //this also takes the column headers into account
            if (e.ColumnIndex != 9 || e.RowIndex == -1)
            {
                return;
            }

            RepaintDeleteButton(e, this.ProductsTable);
        }

        //the delete row functionality
        private void ProductsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (
                !(this.ProductsTable.Columns[e.ColumnIndex] is DataGridViewButtonColumn) ||
                e.RowIndex < 0 ||
                e.RowIndex >= this.ProductsTable.Rows.Count - 1)
            {
                return;
            }

            Result deleteResult =
                ProductsTableController.RecordDeletionRequested(
                    new DataGridViewRowCancelEventArgs(this.ProductsTable.Rows[e.RowIndex]));

            if (deleteResult.IsSuccess)
            {
                this.ProductsTable.Rows.RemoveAt(e.RowIndex);
                TogglePendingSaveVisibility(true);
            }
            else
            {
                Logger.LogError(deleteResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Sales table
        private void SalesTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            SalesTableController.RowAdded();
        }

        private void SalesTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Result deleteResult = SalesTableController.RecordDeletionRequested(e);
            if (!deleteResult.IsSuccess)
            {
                Logger.LogError(deleteResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            TogglePendingSaveVisibility(true);
        }

        private void SalesTable_SelectionChanged(object sender, EventArgs e)
        {
            SalesTableController.SelectionChanged();
        }

        private void SalesTable_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            SalesTableController.CellValueRequested(e);
        }

        private void SalesTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
            CreateSaleWindow wnd = new CreateSaleWindow();

            //if it's NOT the last row
            if (e.RowIndex != this.SalesTable.Rows.Count - 1)
            {
                wnd.SaleIdToModify = (long)this.SalesTable.Rows[e.RowIndex].Cells[0].Value;
            }

            DialogResult result = wnd.ShowDialog();
            if (result == DialogResult.OK)
            {
                TogglePendingSaveVisibility(true);
            }
        }

        private void SalesTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //this also takes the column headers into account
            if (e.ColumnIndex != 5 || e.RowIndex == -1)
            {
                return;
            }

            RepaintDeleteButton(e, this.SalesTable);
        }

        //the delete row functionality
        private void SalesTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (
                !(this.SalesTable.Columns[e.ColumnIndex] is DataGridViewButtonColumn) ||
                e.RowIndex < 0 ||
                e.RowIndex >= this.SalesTable.Rows.Count - 1)
            {
                return;
            }

            SalesTable_UserDeletingRow(
                sender,
                new DataGridViewRowCancelEventArgs(this.SalesTable.Rows[e.RowIndex]));
            this.SalesTable.Rows.RemoveAt(e.RowIndex);
        }
        #endregion


        #region Categories
        private void DeleteCategoriesButton_Click(object sender, EventArgs e)
        {
            bool hasModifiedCategories = false;

            foreach (object category in this.CategoriesListBox.SelectedItems)
            {
                ValueResult<ProductCategory> categoryResult =
                    CategoriesCache.SearchSingleCategory(category.ToString());
                if (!categoryResult.IsSuccess)
                {
                    Logger.LogError(categoryResult.ResultingError.Description);
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    continue;
                }

                bool hasDependentProducts = ProductCache.GetAllProducts()
                    .Where((product) => product.CategoryID == categoryResult.Value.ID).Any();
                if (hasDependentProducts)
                {
                    MessageBox.Show(
                        string.Format(Messages.DEPENDENT_PRODUCTS_ERROR_TEXT, categoryResult.Value.Name),
                        Messages.DEPENDENT_PRODUCTS_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    continue;
                }

                Result deleteResult = CategoriesCache.DeleteCategory(categoryResult.Value.ID);
                if (!deleteResult.IsSuccess)
                {
                    Logger.LogError(deleteResult.ResultingError.Description);
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                hasModifiedCategories = true;
            }

            this.CategoriesListBox.ClearSelected();
            RepopulateCategoriesListBox();
            TogglePendingSaveVisibility(PendingSavesExist || hasModifiedCategories);
        }

        private void AddOrUpdateCategoryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AddCategoryTextBox.Text))
            {
                MessageBox.Show(
                    Messages.VALIDATION_CATEGORY_NAME_EMPTY,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.CategoriesListBox.SelectedItems.Count == 0)
            {
                Result addResult =
                    CategoriesCache.AddCategory(
                        new ProductCategory()
                        {
                            Name = this.AddCategoryTextBox.Text
                        });

                if (!addResult.IsSuccess)
                {
                    //explain that if they tried to add a category that previously existed,
                    //they must save changes before trying to add a category with the same name
                    MessageBox.Show(
                        Messages.CATEGORY_SAVE_NEEDED_TEXT,
                        Messages.CATEGORY_SAVE_NEEDED_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else if (this.CategoriesListBox.SelectedItems.Count == 1)
            {
                ValueResult<ProductCategory> previousValueResult =
                    CategoriesCache.SearchSingleCategory(this.CategoriesListBox.SelectedItems[0].ToString());
                if (!previousValueResult.IsSuccess)
                {
                    Logger.LogError(previousValueResult.ResultingError.Description);
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Result updateResult =
                    CategoriesCache.UpdateCategory(
                        new ProductCategory()
                        {
                            ID = previousValueResult.Value.ID,
                            Name = this.AddCategoryTextBox.Text
                        });

                if (!updateResult.IsSuccess)
                {
                    Logger.LogError(updateResult.ResultingError.Description);
                    //ERROR
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    Messages.VALIDATION_MULTIPLE_CATEGORIES_SELECTED_TEXT,
                    Messages.VALIDATION_MULTIPLE_CATEGORIES_SELECTED_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            this.CategoriesListBox.ClearSelected();
            RepopulateCategoriesListBox();
            this.AddCategoryTextBox.Text = "";
            TogglePendingSaveVisibility(true);
        }

        private void CategoriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CategoriesListBox.SelectedItems.Count == 0)
            {
                this.AddOrUpdateCategoryLabel.Text = Strings.Add_a_new_category;
                this.AddOrUpdateCategoryButton.Text = Strings.Add;

                this.AddCategoryTextBox.Enabled = true;
                this.AddOrUpdateCategoryButton.Enabled = true;
                this.DeleteCategoriesButton.Enabled = false;
            }
            else if (this.CategoriesListBox.SelectedItems.Count == 1)
            {
                this.AddOrUpdateCategoryLabel.Text = Strings.New_name_for_the_selected_category;
                this.AddOrUpdateCategoryButton.Text = Strings.Modify;

                this.AddCategoryTextBox.Enabled = true;
                this.AddOrUpdateCategoryButton.Enabled = true;
                this.DeleteCategoriesButton.Enabled = true;
            }
            else
            {
                this.AddOrUpdateCategoryLabel.Text = Strings.Can_only_update_a_single_category_simultaneously_;
                this.AddOrUpdateCategoryButton.Text = Strings.Modify;

                this.AddCategoryTextBox.Enabled = false;
                this.AddOrUpdateCategoryButton.Enabled = false;
                this.DeleteCategoriesButton.Enabled = true;
            }
        }

        private void DeselectButton_Click(object sender, EventArgs e)
        {
            this.CategoriesListBox.ClearSelected();
        }

        private void AddCategoryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddOrUpdateCategoryButton_Click(null, null);
                e.Handled = true;
                TogglePendingSaveVisibility(true);
            }
        }

        private void RepopulateCategoriesListBox()
        {
            this.CategoriesListBox.Items.Clear();

            CategoriesCache.GetAllCategories().ForEach((category) =>
            {
                //the default category ("") can not be edited
                if (category.Name == "")
                {
                    return;
                }

                //search
                if (this.SearchBar.Text != "" &&
                    !category.Name.ToLower().Contains(this.SearchBar.Text.ToLower()))
                {
                    return;
                }

                this.CategoriesListBox.Items.Add(category.Name);
            });
        }
        #endregion


        #region Tools
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await SaveChangesAsync();
        }

        private async void DiscardChangesButton_Click(object sender, EventArgs e)
        {
            if (!PendingSavesExist)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                Messages.DISCARD_CHANGES_TEXT,
                Messages.DISCARD_CHANGES_TITLE,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            Task[] loadTasks = new Task[3];
            loadTasks[0] = ProductCache.RegenerateCacheFromDBAsync();
            loadTasks[1] = SalesCache.RegenerateCacheFromDBAsync();
            loadTasks[2] = CategoriesCache.RegenerateCacheFromDBAsync();
            await Task.WhenAll(loadTasks);

            RefreshData();
            TogglePendingSaveVisibility(false);
        }

        private async Task SaveChangesAsync()
        {
            //ORDER IS IMPORTANT!!! categories, then products, then sales
            Result saveResult;
            saveResult = await CategoriesCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                //MessageBox.Show("Error on categories save");
                Logger.LogError(saveResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            saveResult = await ProductCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                //MessageBox.Show("Error on products save");
                Logger.LogError(saveResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            saveResult = await SalesCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                //MessageBox.Show("Error on sales save");
                Logger.LogError(saveResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            ProductsTableController.RepopulateTable();
            SalesTableController.RepopulateTable();
            TogglePendingSaveVisibility(false);
        }

        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            _isSearchRelevant = this.SearchBar.Text.Length > 0;
            RepaintSearchBar();

            //to trigger a table refresh
            TabControl_SelectedIndexChanged(null, null);
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            _isSearchRelevant = false;
            RepaintSearchBar();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            _isSearchRelevant = this.SearchBar.Text.Length > 0;
            RepaintSearchBar();

            //to trigger a table refresh
            TabControl_SelectedIndexChanged(null, null);
        }

        private void RepaintSearchBar()
        {
            this.SearchBar.BackColor =
                _isSearchRelevant ?
                Color.FromArgb(0x1a, 0x23, 0x7e) :
                Color.FromArgb(0x21, 0x21, 0x21);
        }
        #endregion


        #region Menu
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RoLangMenuItem_Click(object sender, EventArgs e)
        {
            if (this.RoLangMenuItem.Checked)
            {
                return;
            }

            this.RoLangMenuItem.Checked = true;
            this.EnLangMenuItem.Checked = false;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ro");
            Translate();

            Settings settings = Settings.Default;
            settings.Language = "ro";
            settings.Save();
        }

        private void EnLangMenuItem_Click(object sender, EventArgs e)
        {
            if (this.EnLangMenuItem.Checked)
            {
                return;
            }

            this.RoLangMenuItem.Checked = false;
            this.EnLangMenuItem.Checked = true;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Translate();

            Settings settings = Settings.Default;
            settings.Language = "en";
            settings.Save();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void ExportSalesMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "sales.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = $"{Strings.XML_File} (*.xml)|*.xml"
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            XmlDocument xmlDocument = ItemsXmlSerializer.SerializeSalesCache();
            if (xmlDocument == null)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ExportAndShow(xmlDocument, saveFileDialog.FileName);
        }

        private void ExportProductsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "products.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = $"{Strings.XML_File} (*.xml)|*.xml"
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            XmlDocument xmlDocument = ItemsXmlSerializer.SerializeProductsCache();
            if (xmlDocument == null)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ExportAndShow(xmlDocument, saveFileDialog.FileName);
        }

        private void ExportCategoriesMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "categories.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = $"{Strings.XML_File} (*.xml)|*.xml"
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            XmlDocument xmlDocument = ItemsXmlSerializer.SerializeCategoriesCache();
            if (xmlDocument == null)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ExportAndShow(xmlDocument, saveFileDialog.FileName);
        }

        private void ExportAndShow(XmlDocument xmlDocument, string filePath)
        {
            bool success = XmlFilesManager.WriteXml(xmlDocument, filePath);
            if (success)
            {
                MessageBox.Show(
                    Messages.EXPORT_SUCCESS,
                    Messages.SUCCESS,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        private void CreateSaleButton_Click(object sender, EventArgs e)
        {
            CreateSaleWindow wnd = new CreateSaleWindow();
            DialogResult result = wnd.ShowDialog();
            if (result == DialogResult.OK)
            {
                TogglePendingSaveVisibility(true);
            }
        }

        private void AccountButton_Click(object sender, EventArgs e)
        {
            AccountManagementWindow accountManagementWindow = new AccountManagementWindow();
            accountManagementWindow.ShowDialog();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.TabControl.SelectedIndex)
            {
                case 0:
                    ProductsTableController.RepopulateTable();
                    break;
                case 1:
                    SalesTableController.RepopulateTable();
                    break;
                case 2:
                    RepopulateCategoriesListBox();
                    break;
            }
        }

        private void RepaintDeleteButton(DataGridViewCellPaintingEventArgs e, DataGridView table)
        {
            if (e.RowIndex >= table.Rows.Count - 1)
            {
                return;
            }

            //apparently WinForms doesn't like the Image to be cached, so we really need to recreate it every time
            Stream deleteIconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                Assembly.GetExecutingAssembly().GetName().ToString().Split(',')[0] + ".DirectAssets.delete_icon.png");
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            Image image = Image.FromStream(deleteIconStream);

            // -4 for the button margins
            int w = image.Width - 4;
            int h = Math.Min(image.Height, table.Rows[e.RowIndex].Height) - 4;
            int x = e.CellBounds.Left + ((e.CellBounds.Width - w) / 2);
            int y = e.CellBounds.Top + ((e.CellBounds.Height - h) / 2);

            e.Graphics.DrawImage(image, new Rectangle(x, y, w, h));
            e.Handled = true;
        }
    }
}
