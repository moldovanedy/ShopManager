namespace ShopManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CreateSaleButton = new System.Windows.Forms.Button();
            this.AppMenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanugageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoLangMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnLangMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberOfProductsLabel = new System.Windows.Forms.Label();
            this.ProductsTable = new System.Windows.Forms.DataGridView();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PricePerKg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCategoryName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProductDeleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Spacer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.ProductsPage = new System.Windows.Forms.TabPage();
            this.ProductPaginationHandler = new System.Windows.Forms.ToolStrip();
            this.SalesPage = new System.Windows.Forms.TabPage();
            this.SalesPaginationHandler = new System.Windows.Forms.ToolStrip();
            this.SalesTable = new System.Windows.Forms.DataGridView();
            this.ProductCategoriesPage = new System.Windows.Forms.TabPage();
            this.DeselectButton = new System.Windows.Forms.Button();
            this.AddOrUpdateCategoryLabel = new System.Windows.Forms.Label();
            this.AddCategoryTextBox = new System.Windows.Forms.TextBox();
            this.AddOrUpdateCategoryButton = new System.Windows.Forms.Button();
            this.DeleteCategoriesButton = new System.Windows.Forms.Button();
            this.CategoriesListBox = new System.Windows.Forms.ListBox();
            this.ToolsContainer = new System.Windows.Forms.ToolStripContainer();
            this.Tools = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.DiscardChangesButton = new System.Windows.Forms.ToolStripButton();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.SaleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleDeleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsTable)).BeginInit();
            this.TabControl.SuspendLayout();
            this.ProductsPage.SuspendLayout();
            this.SalesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTable)).BeginInit();
            this.ProductCategoriesPage.SuspendLayout();
            this.ToolsContainer.ContentPanel.SuspendLayout();
            this.ToolsContainer.SuspendLayout();
            this.Tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreateSaleButton
            // 
            this.CreateSaleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateSaleButton.AutoSize = true;
            this.CreateSaleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CreateSaleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.CreateSaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateSaleButton.ForeColor = System.Drawing.Color.White;
            this.CreateSaleButton.Location = new System.Drawing.Point(772, 44);
            this.CreateSaleButton.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.CreateSaleButton.Name = "CreateSaleButton";
            this.CreateSaleButton.Size = new System.Drawing.Size(93, 28);
            this.CreateSaleButton.TabIndex = 0;
            this.CreateSaleButton.Text = "Create sale";
            this.CreateSaleButton.UseVisualStyleBackColor = false;
            this.CreateSaleButton.Click += new System.EventHandler(this.CreateSaleButton_Click);
            // 
            // AppMenuBar
            // 
            this.AppMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.HelpMenuItem});
            this.AppMenuBar.Location = new System.Drawing.Point(0, 0);
            this.AppMenuBar.Name = "AppMenuBar";
            this.AppMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.AppMenuBar.Size = new System.Drawing.Size(884, 24);
            this.AppMenuBar.TabIndex = 1;
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LanugageMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // LanugageMenuItem
            // 
            this.LanugageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RoLangMenuItem,
            this.EnLangMenuItem});
            this.LanugageMenuItem.Name = "LanugageMenuItem";
            this.LanugageMenuItem.Size = new System.Drawing.Size(134, 22);
            this.LanugageMenuItem.Text = "Language";
            // 
            // RoLangMenuItem
            // 
            this.RoLangMenuItem.Name = "RoLangMenuItem";
            this.RoLangMenuItem.Size = new System.Drawing.Size(140, 22);
            this.RoLangMenuItem.Text = "Română (ro)";
            this.RoLangMenuItem.Click += new System.EventHandler(this.RoLangMenuItem_Click);
            // 
            // EnLangMenuItem
            // 
            this.EnLangMenuItem.Name = "EnLangMenuItem";
            this.EnLangMenuItem.Size = new System.Drawing.Size(140, 22);
            this.EnLangMenuItem.Text = "English (en)";
            this.EnLangMenuItem.Click += new System.EventHandler(this.EnLangMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpMenuItem.Text = "Help";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutMenuItem.Size = new System.Drawing.Size(126, 22);
            this.AboutMenuItem.Text = "About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // NumberOfProductsLabel
            // 
            this.NumberOfProductsLabel.AutoSize = true;
            this.NumberOfProductsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfProductsLabel.ForeColor = System.Drawing.Color.White;
            this.NumberOfProductsLabel.Location = new System.Drawing.Point(12, 46);
            this.NumberOfProductsLabel.Name = "NumberOfProductsLabel";
            this.NumberOfProductsLabel.Size = new System.Drawing.Size(177, 24);
            this.NumberOfProductsLabel.TabIndex = 3;
            this.NumberOfProductsLabel.Text = "Number of products";
            // 
            // ProductsTable
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.ProductsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductsTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductsTable.ColumnHeadersHeight = 40;
            this.ProductsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductDisplayName,
            this.ProductDescription,
            this.Price,
            this.PricePerKg,
            this.ProductPurchaseDate,
            this.ProductExpiryDate,
            this.ProductQuantity,
            this.ProductCategoryName,
            this.ProductDeleteRow,
            this.Spacer});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductsTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductsTable.EnableHeadersVisualStyles = false;
            this.ProductsTable.GridColor = System.Drawing.Color.White;
            this.ProductsTable.Location = new System.Drawing.Point(0, 0);
            this.ProductsTable.Name = "ProductsTable";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.ProductsTable.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ProductsTable.Size = new System.Drawing.Size(876, 315);
            this.ProductsTable.TabIndex = 4;
            this.ProductsTable.VirtualMode = true;
            this.ProductsTable.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.ProductsTable_CellValueNeeded);
            this.ProductsTable.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.ProductsTable_CellValuePushed);
            this.ProductsTable.RowDirtyStateNeeded += new System.Windows.Forms.QuestionEventHandler(this.ProductsTable_RowDirtyStateNeeded);
            this.ProductsTable.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ProductsTable_UserAddedRow);
            this.ProductsTable.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ProductsTable_UserDeletingRow);
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "ID";
            this.ProductID.MinimumWidth = 20;
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 52;
            // 
            // ProductDisplayName
            // 
            this.ProductDisplayName.HeaderText = "Name";
            this.ProductDisplayName.MaxInputLength = 255;
            this.ProductDisplayName.MinimumWidth = 20;
            this.ProductDisplayName.Name = "ProductDisplayName";
            this.ProductDisplayName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductDisplayName.Width = 80;
            // 
            // ProductDescription
            // 
            this.ProductDescription.HeaderText = "Description";
            this.ProductDescription.MaxInputLength = 65535;
            this.ProductDescription.MinimumWidth = 20;
            this.ProductDescription.Name = "ProductDescription";
            this.ProductDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductDescription.Width = 130;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 45;
            this.Price.Name = "Price";
            this.Price.Width = 45;
            // 
            // PricePerKg
            // 
            this.PricePerKg.HeaderText = "Price per KG";
            this.PricePerKg.MinimumWidth = 70;
            this.PricePerKg.Name = "PricePerKg";
            this.PricePerKg.Width = 70;
            // 
            // ProductPurchaseDate
            // 
            this.ProductPurchaseDate.HeaderText = "Purchase date";
            this.ProductPurchaseDate.MinimumWidth = 30;
            this.ProductPurchaseDate.Name = "ProductPurchaseDate";
            this.ProductPurchaseDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductPurchaseDate.Width = 130;
            // 
            // ProductExpiryDate
            // 
            this.ProductExpiryDate.HeaderText = "Expiry date";
            this.ProductExpiryDate.MinimumWidth = 30;
            this.ProductExpiryDate.Name = "ProductExpiryDate";
            this.ProductExpiryDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductExpiryDate.Width = 109;
            // 
            // ProductQuantity
            // 
            this.ProductQuantity.HeaderText = "Quantity";
            this.ProductQuantity.MinimumWidth = 20;
            this.ProductQuantity.Name = "ProductQuantity";
            this.ProductQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductQuantity.Width = 90;
            // 
            // ProductCategoryName
            // 
            this.ProductCategoryName.HeaderText = "Category";
            this.ProductCategoryName.Items.AddRange(new object[] {
            "Fructe",
            "Legume",
            "Electronice"});
            this.ProductCategoryName.MinimumWidth = 20;
            this.ProductCategoryName.Name = "ProductCategoryName";
            this.ProductCategoryName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductCategoryName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProductCategoryName.Width = 94;
            // 
            // ProductDeleteRow
            // 
            this.ProductDeleteRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProductDeleteRow.HeaderText = "";
            this.ProductDeleteRow.MinimumWidth = 25;
            this.ProductDeleteRow.Name = "ProductDeleteRow";
            this.ProductDeleteRow.ReadOnly = true;
            this.ProductDeleteRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductDeleteRow.Text = "";
            this.ProductDeleteRow.Width = 25;
            // 
            // Spacer
            // 
            this.Spacer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spacer.HeaderText = "";
            this.Spacer.Name = "Spacer";
            this.Spacer.ReadOnly = true;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl.Controls.Add(this.ProductsPage);
            this.TabControl.Controls.Add(this.SalesPage);
            this.TabControl.Controls.Add(this.ProductCategoriesPage);
            this.TabControl.Location = new System.Drawing.Point(0, 120);
            this.TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(884, 369);
            this.TabControl.TabIndex = 5;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // ProductsPage
            // 
            this.ProductsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ProductsPage.Controls.Add(this.ProductPaginationHandler);
            this.ProductsPage.Controls.Add(this.ProductsTable);
            this.ProductsPage.Location = new System.Drawing.Point(4, 25);
            this.ProductsPage.Margin = new System.Windows.Forms.Padding(0);
            this.ProductsPage.Name = "ProductsPage";
            this.ProductsPage.Size = new System.Drawing.Size(876, 340);
            this.ProductsPage.TabIndex = 0;
            this.ProductsPage.Text = "Products";
            // 
            // ProductPaginationHandler
            // 
            this.ProductPaginationHandler.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProductPaginationHandler.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ProductPaginationHandler.Location = new System.Drawing.Point(0, 315);
            this.ProductPaginationHandler.Name = "ProductPaginationHandler";
            this.ProductPaginationHandler.Size = new System.Drawing.Size(876, 25);
            this.ProductPaginationHandler.TabIndex = 5;
            this.ProductPaginationHandler.Text = "toolStrip1";
            // 
            // SalesPage
            // 
            this.SalesPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.SalesPage.Controls.Add(this.SalesPaginationHandler);
            this.SalesPage.Controls.Add(this.SalesTable);
            this.SalesPage.Location = new System.Drawing.Point(4, 25);
            this.SalesPage.Margin = new System.Windows.Forms.Padding(0);
            this.SalesPage.Name = "SalesPage";
            this.SalesPage.Size = new System.Drawing.Size(876, 340);
            this.SalesPage.TabIndex = 1;
            this.SalesPage.Text = "Sales";
            // 
            // SalesPaginationHandler
            // 
            this.SalesPaginationHandler.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SalesPaginationHandler.Location = new System.Drawing.Point(0, 315);
            this.SalesPaginationHandler.Name = "SalesPaginationHandler";
            this.SalesPaginationHandler.Size = new System.Drawing.Size(876, 25);
            this.SalesPaginationHandler.TabIndex = 6;
            this.SalesPaginationHandler.Text = "toolStrip1";
            // 
            // SalesTable
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.SalesTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.SalesTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SalesTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.SalesTable.ColumnHeadersHeight = 40;
            this.SalesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleID,
            this.SoldProduct,
            this.ProductCategory,
            this.SaleQuantity,
            this.Total,
            this.SaleDeleteRow,
            this.dataGridViewTextBoxColumn7});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SalesTable.DefaultCellStyle = dataGridViewCellStyle8;
            this.SalesTable.EnableHeadersVisualStyles = false;
            this.SalesTable.GridColor = System.Drawing.Color.White;
            this.SalesTable.Location = new System.Drawing.Point(0, 0);
            this.SalesTable.Name = "SalesTable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SalesTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.SalesTable.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.SalesTable.Size = new System.Drawing.Size(876, 315);
            this.SalesTable.TabIndex = 5;
            this.SalesTable.VirtualMode = true;
            this.SalesTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.SalesTable_CellBeginEdit);
            this.SalesTable.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.SalesTable_CellValueNeeded);
            this.SalesTable.SelectionChanged += new System.EventHandler(this.SalesTable_SelectionChanged);
            this.SalesTable.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.SalesTable_UserAddedRow);
            this.SalesTable.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.SalesTable_UserDeletingRow);
            // 
            // ProductCategoriesPage
            // 
            this.ProductCategoriesPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ProductCategoriesPage.Controls.Add(this.DeselectButton);
            this.ProductCategoriesPage.Controls.Add(this.AddOrUpdateCategoryLabel);
            this.ProductCategoriesPage.Controls.Add(this.AddCategoryTextBox);
            this.ProductCategoriesPage.Controls.Add(this.AddOrUpdateCategoryButton);
            this.ProductCategoriesPage.Controls.Add(this.DeleteCategoriesButton);
            this.ProductCategoriesPage.Controls.Add(this.CategoriesListBox);
            this.ProductCategoriesPage.Location = new System.Drawing.Point(4, 25);
            this.ProductCategoriesPage.Margin = new System.Windows.Forms.Padding(0);
            this.ProductCategoriesPage.Name = "ProductCategoriesPage";
            this.ProductCategoriesPage.Size = new System.Drawing.Size(876, 340);
            this.ProductCategoriesPage.TabIndex = 2;
            this.ProductCategoriesPage.Text = "Product categories";
            // 
            // DeselectButton
            // 
            this.DeselectButton.AutoSize = true;
            this.DeselectButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.DeselectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeselectButton.ForeColor = System.Drawing.Color.White;
            this.DeselectButton.Location = new System.Drawing.Point(210, 39);
            this.DeselectButton.Name = "DeselectButton";
            this.DeselectButton.Size = new System.Drawing.Size(125, 28);
            this.DeselectButton.TabIndex = 5;
            this.DeselectButton.Text = "Deselect";
            this.DeselectButton.UseVisualStyleBackColor = false;
            this.DeselectButton.Click += new System.EventHandler(this.DeselectButton_Click);
            // 
            // AddOrUpdateCategoryLabel
            // 
            this.AddOrUpdateCategoryLabel.AutoSize = true;
            this.AddOrUpdateCategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddOrUpdateCategoryLabel.ForeColor = System.Drawing.Color.White;
            this.AddOrUpdateCategoryLabel.Location = new System.Drawing.Point(210, 90);
            this.AddOrUpdateCategoryLabel.Name = "AddOrUpdateCategoryLabel";
            this.AddOrUpdateCategoryLabel.Size = new System.Drawing.Size(137, 17);
            this.AddOrUpdateCategoryLabel.TabIndex = 4;
            this.AddOrUpdateCategoryLabel.Text = "Add a new category:";
            // 
            // AddCategoryTextBox
            // 
            this.AddCategoryTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.AddCategoryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCategoryTextBox.ForeColor = System.Drawing.Color.White;
            this.AddCategoryTextBox.Location = new System.Drawing.Point(210, 115);
            this.AddCategoryTextBox.Name = "AddCategoryTextBox";
            this.AddCategoryTextBox.Size = new System.Drawing.Size(200, 23);
            this.AddCategoryTextBox.TabIndex = 3;
            this.AddCategoryTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddCategoryTextBox_KeyDown);
            // 
            // AddOrUpdateCategoryButton
            // 
            this.AddOrUpdateCategoryButton.AutoSize = true;
            this.AddOrUpdateCategoryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.AddOrUpdateCategoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddOrUpdateCategoryButton.ForeColor = System.Drawing.Color.White;
            this.AddOrUpdateCategoryButton.Location = new System.Drawing.Point(425, 113);
            this.AddOrUpdateCategoryButton.Name = "AddOrUpdateCategoryButton";
            this.AddOrUpdateCategoryButton.Size = new System.Drawing.Size(75, 27);
            this.AddOrUpdateCategoryButton.TabIndex = 2;
            this.AddOrUpdateCategoryButton.Text = "Add";
            this.AddOrUpdateCategoryButton.UseVisualStyleBackColor = false;
            this.AddOrUpdateCategoryButton.Click += new System.EventHandler(this.AddOrUpdateCategoryButton_Click);
            // 
            // DeleteCategoriesButton
            // 
            this.DeleteCategoriesButton.AutoSize = true;
            this.DeleteCategoriesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DeleteCategoriesButton.Enabled = false;
            this.DeleteCategoriesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteCategoriesButton.ForeColor = System.Drawing.Color.White;
            this.DeleteCategoriesButton.Location = new System.Drawing.Point(210, 5);
            this.DeleteCategoriesButton.Name = "DeleteCategoriesButton";
            this.DeleteCategoriesButton.Size = new System.Drawing.Size(125, 28);
            this.DeleteCategoriesButton.TabIndex = 1;
            this.DeleteCategoriesButton.Text = "Delete selected";
            this.DeleteCategoriesButton.UseVisualStyleBackColor = false;
            this.DeleteCategoriesButton.Click += new System.EventHandler(this.DeleteCategoriesButton_Click);
            // 
            // CategoriesListBox
            // 
            this.CategoriesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.CategoriesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CategoriesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoriesListBox.ForeColor = System.Drawing.Color.White;
            this.CategoriesListBox.FormattingEnabled = true;
            this.CategoriesListBox.ItemHeight = 16;
            this.CategoriesListBox.Location = new System.Drawing.Point(0, 0);
            this.CategoriesListBox.Name = "CategoriesListBox";
            this.CategoriesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CategoriesListBox.Size = new System.Drawing.Size(200, 162);
            this.CategoriesListBox.Sorted = true;
            this.CategoriesListBox.TabIndex = 0;
            this.CategoriesListBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesListBox_SelectedIndexChanged);
            // 
            // ToolsContainer
            // 
            this.ToolsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // ToolsContainer.ContentPanel
            // 
            this.ToolsContainer.ContentPanel.Controls.Add(this.Tools);
            this.ToolsContainer.ContentPanel.Size = new System.Drawing.Size(884, 5);
            this.ToolsContainer.Location = new System.Drawing.Point(0, 90);
            this.ToolsContainer.Name = "ToolsContainer";
            this.ToolsContainer.Size = new System.Drawing.Size(884, 30);
            this.ToolsContainer.TabIndex = 6;
            this.ToolsContainer.Text = "toolStripContainer1";
            // 
            // Tools
            // 
            this.Tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveButton,
            this.DiscardChangesButton});
            this.Tools.Location = new System.Drawing.Point(0, 0);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(884, 5);
            this.Tools.TabIndex = 0;
            this.Tools.Text = "toolStrip1";
            // 
            // SaveButton
            // 
            this.SaveButton.AccessibleDescription = "Save the current changes";
            this.SaveButton.AccessibleName = "Save";
            this.SaveButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(51, 2);
            this.SaveButton.Text = "Save";
            this.SaveButton.ToolTipText = "Save changes";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DiscardChangesButton
            // 
            this.DiscardChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("DiscardChangesButton.Image")));
            this.DiscardChangesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DiscardChangesButton.Name = "DiscardChangesButton";
            this.DiscardChangesButton.Size = new System.Drawing.Size(113, 2);
            this.DiscardChangesButton.Text = "Discard changes";
            this.DiscardChangesButton.ToolTipText = "Discard all the changes made from the last save";
            this.DiscardChangesButton.Click += new System.EventHandler(this.DiscardChangesButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 489);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(884, 22);
            this.StatusBar.TabIndex = 7;
            this.StatusBar.Text = "Status";
            // 
            // SaleID
            // 
            this.SaleID.HeaderText = "ID";
            this.SaleID.MinimumWidth = 20;
            this.SaleID.Name = "SaleID";
            this.SaleID.ReadOnly = true;
            this.SaleID.Width = 52;
            // 
            // SoldProduct
            // 
            this.SoldProduct.HeaderText = "Product";
            this.SoldProduct.MaxInputLength = 255;
            this.SoldProduct.MinimumWidth = 20;
            this.SoldProduct.Name = "SoldProduct";
            this.SoldProduct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SoldProduct.Width = 160;
            // 
            // ProductCategory
            // 
            this.ProductCategory.HeaderText = "Product category";
            this.ProductCategory.MinimumWidth = 20;
            this.ProductCategory.Name = "ProductCategory";
            this.ProductCategory.ReadOnly = true;
            // 
            // SaleQuantity
            // 
            this.SaleQuantity.HeaderText = "Quantity";
            this.SaleQuantity.MinimumWidth = 20;
            this.SaleQuantity.Name = "SaleQuantity";
            this.SaleQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SaleQuantity.Width = 90;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 20;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 80;
            // 
            // SaleDeleteRow
            // 
            this.SaleDeleteRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaleDeleteRow.HeaderText = "";
            this.SaleDeleteRow.MinimumWidth = 25;
            this.SaleDeleteRow.Name = "SaleDeleteRow";
            this.SaleDeleteRow.ReadOnly = true;
            this.SaleDeleteRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SaleDeleteRow.Text = "";
            this.SaleDeleteRow.Width = 25;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.ToolsContainer);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.NumberOfProductsLabel);
            this.Controls.Add(this.AppMenuBar);
            this.Controls.Add(this.CreateSaleButton);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.AppMenuBar;
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "MainForm";
            this.Text = "Shop Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.AppMenuBar.ResumeLayout(false);
            this.AppMenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsTable)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.ProductsPage.ResumeLayout(false);
            this.ProductsPage.PerformLayout();
            this.SalesPage.ResumeLayout(false);
            this.SalesPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTable)).EndInit();
            this.ProductCategoriesPage.ResumeLayout(false);
            this.ProductCategoriesPage.PerformLayout();
            this.ToolsContainer.ContentPanel.ResumeLayout(false);
            this.ToolsContainer.ContentPanel.PerformLayout();
            this.ToolsContainer.ResumeLayout(false);
            this.ToolsContainer.PerformLayout();
            this.Tools.ResumeLayout(false);
            this.Tools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateSaleButton;
        private System.Windows.Forms.MenuStrip AppMenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LanugageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RoLangMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnLangMenuItem;
        private System.Windows.Forms.Label NumberOfProductsLabel;
        private System.Windows.Forms.DataGridView ProductsTable;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage ProductsPage;
        private System.Windows.Forms.TabPage SalesPage;
        private System.Windows.Forms.DataGridView SalesTable;
        private System.Windows.Forms.TabPage ProductCategoriesPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripContainer ToolsContainer;
        private System.Windows.Forms.ToolStrip Tools;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStrip ProductPaginationHandler;
        private System.Windows.Forms.ToolStrip SalesPaginationHandler;
        private System.Windows.Forms.ToolStripButton DiscardChangesButton;
        private System.Windows.Forms.ListBox CategoriesListBox;
        private System.Windows.Forms.Button DeleteCategoriesButton;
        private System.Windows.Forms.Button AddOrUpdateCategoryButton;
        private System.Windows.Forms.TextBox AddCategoryTextBox;
        private System.Windows.Forms.Label AddOrUpdateCategoryLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductDisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn PricePerKg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPurchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductQuantity;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProductCategoryName;
        private System.Windows.Forms.DataGridViewButtonColumn ProductDeleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spacer;
        private System.Windows.Forms.Button DeselectButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn SaleDeleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}

