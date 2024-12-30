using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ShopManager.Controller.CacheManager;
using ShopManager.Controllers;
using ShopManager.Extensions;
using ShopManager.Model.DBManager;
using ShopManager.Resources.Locale;
using ShopManager.Utils;

namespace ShopManager
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }

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
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ro");

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
            //await Task.Delay(500);
            await ProductCache.RegenerateCacheFromDBAsync(0);
            ProductsTableController.RepopulateTable();
        }

        internal DataGridView GetProductsTableUI()
        {
            return this.ProductsTable;
        }

        internal DataGridView GetSalesTableUI()
        {
            return this.SalesTable;
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
            ControlLocalization.Translate(this);
            this.FileMenuItem.Text = Strings.File;
            this.HelpMenuItem.Text = Strings.Help;

            this.SaveButton.Text = Strings.Save;
            this.SaveButton.ToolTipText = Strings.Save_changes;
            this.DiscardChangesButton.Text = Strings.Discard_changes;
            this.DiscardChangesButton.ToolTipText = Strings.Discard_all_the_changes_made_from_the_last_save;
        }

        #region Products table
        private void ProductsTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ProductsTableController.RowAdded();
        }

        private void ProductsTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            ProductsTableController.RecordDeletionRequested(e);
        }

        private void ProductsTable_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            ProductsTableController.CellValueSubmitted(e);
        }

        private void ProductsTable_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            ProductsTableController.CellValueRequested(e);
        }

        private void ProductsTable_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
        {
            e.Response = this.ProductsTable.IsCurrentCellDirty;
        }

        private void ProductsTable_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            Debug.WriteLine("Row editing cancelled!");
        }
        #endregion


        #region Tools
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await ProductCache.FlushCacheToDBAsync();
            ProductsTableController.RepopulateTable();
        }
        #endregion
    }
}
