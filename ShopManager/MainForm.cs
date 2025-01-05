﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Controllers;
using ShopManager.Extensions;
using ShopManager.Model.DataModels;
using ShopManager.Model.DBManager;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }

        public static bool PendingSavesExist { get; private set; } = false;

        private readonly Color NormalColor = Color.FromArgb(0xff, 0x42, 0x42, 0x42);
        private readonly Color LighterColor1 = Color.FromArgb(0xff, 0x51, 0x51, 0x51);

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
            RoLangMenuItem_Click(null, null);

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
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            await MasterDBManager.InitializeDBAsync();

            Task[] loadTasks = new Task[3];
            loadTasks[0] = ProductCache.RegenerateCacheFromDBAsync(0);
            loadTasks[1] = SalesCache.RegenerateCacheFromDBAsync(0);
            loadTasks[2] = CategoriesCache.RegenerateCacheFromDBAsync();
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


        private void Translate()
        {
            TogglePendingSaveVisibility(false);

            //top-level
            this.NumberOfProductsLabel.Text = Strings.Number_of_products;
            this.CreateSaleButton.Text = Strings.Create_sale;
            this.TabControl.TabPages[0].Text = Strings.Products;
            this.TabControl.TabPages[1].Text = Strings.Sales;
            this.TabControl.TabPages[2].Text = Strings.Product_categories;

            //menu bar
            this.FileMenuItem.Text = Strings.File;
            this.LanugageMenuItem.Text = Strings.Language;
            this.ExitMenuItem.Text = Strings.Exit;

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
            this.SalesTable.Columns[2].HeaderText = Strings.Quantity;

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

        #region Products table
        private void ProductsTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ProductsTableController.RowAdded();
        }

        private void ProductsTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            ProductsTableController.RecordDeletionRequested(e);
            TogglePendingSaveVisibility(true);
        }

        private void ProductsTable_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            ProductsTableController.CellValueSubmitted(e);

            if (!ProductsTableController.IsAddingFromCode)
            {
                TogglePendingSaveVisibility(true);
            }
        }

        private void ProductsTable_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            ProductsTableController.CellValueRequested(e);
        }

        private void ProductsTable_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
        {
            e.Response = this.ProductsTable.IsCurrentCellDirty;
        }
        #endregion


        #region Sales table
        private void SalesTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            SalesTableController.RowAdded();
        }

        private void SalesTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            SalesTableController.RecordDeletionRequested(e);
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
        #endregion


        #region Categories
        private void DeleteCategoriesButton_Click(object sender, EventArgs e)
        {
            foreach (object category in this.CategoriesListBox.SelectedItems)
            {
                ValueResult<ProductCategory> categoryResult =
                    CategoriesCache.SearchSingleCategory(category.ToString());
                if (!categoryResult.IsSuccess)
                {
                    //ERROR (continue after)
                    continue;
                }

                Result deleteResult = CategoriesCache.DeleteCategory(categoryResult.Value.ID);
                if (!deleteResult.IsSuccess)
                {
                    //ERROR (continue after)
                }
            }

            this.CategoriesListBox.ClearSelected();
            RepopulateCategoriesListBox();
            TogglePendingSaveVisibility(true);
        }

        private void AddOrUpdateCategoryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AddCategoryTextBox.Text))
            {
                //ERROR
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
                    //ERROR
                }
            }
            else if (this.CategoriesListBox.SelectedItems.Count == 1)
            {
                ValueResult<ProductCategory> previousValueResult =
                    CategoriesCache.SearchSingleCategory(this.CategoriesListBox.SelectedItems[0].ToString());
                if (!previousValueResult.IsSuccess)
                {
                    //ERROR
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
                    //ERROR
                }
            }
            else
            {
                //ERROR
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
            }
            e.Handled = true;
            TogglePendingSaveVisibility(true);
        }

        private void RepopulateCategoriesListBox()
        {
            this.CategoriesListBox.Items.Clear();

            CategoriesCache.GetAllCategories().ForEach((category) =>
            {
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
            loadTasks[0] = ProductCache.RegenerateCacheFromDBAsync(0);
            loadTasks[1] = SalesCache.RegenerateCacheFromDBAsync(0);
            loadTasks[2] = CategoriesCache.RegenerateCacheFromDBAsync();
            await Task.WhenAll(loadTasks);

            RefreshData();
            TogglePendingSaveVisibility(false);
        }

        private async Task SaveChangesAsync()
        {
            Result saveResult;
            saveResult = await ProductCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show("Error on products save");
            }

            saveResult = await SalesCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show("Error on sales save");
            }

            saveResult = await CategoriesCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show("Error on categories save");
            }

            ProductsTableController.RepopulateTable();
            SalesTableController.RepopulateTable();
            TogglePendingSaveVisibility(false);
        }
        #endregion

        #region Menu
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RoLangMenuItem_Click(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "ro")
            {
                return;
            }

            this.RoLangMenuItem.Checked = true;
            this.EnLangMenuItem.Checked = false;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ro");
            Translate();
        }

        private void EnLangMenuItem_Click(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "en")
            {
                return;
            }

            this.RoLangMenuItem.Checked = false;
            this.EnLangMenuItem.Checked = true;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Translate();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
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
    }
}
