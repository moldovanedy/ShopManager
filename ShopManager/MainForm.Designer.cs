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
            this.CreateSaleButton = new System.Windows.Forms.Button();
            this.AppMenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanugageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoLangMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnLangMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProductButton = new System.Windows.Forms.Button();
            this.NumberOfProductsLabel = new System.Windows.Forms.Label();
            this.ProductsTable = new System.Windows.Forms.DataGridView();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCategoryName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProductDeleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Spacer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.ProductsPage = new System.Windows.Forms.TabPage();
            this.SalesPage = new System.Windows.Forms.TabPage();
            this.SalesTable = new System.Windows.Forms.DataGridView();
            this.SaleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleDeleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCategoriesPage = new System.Windows.Forms.TabPage();
            this.AppMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsTable)).BeginInit();
            this.TabControl.SuspendLayout();
            this.ProductsPage.SuspendLayout();
            this.SalesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTable)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateSaleButton
            // 
            this.CreateSaleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateSaleButton.AutoSize = true;
            this.CreateSaleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.CreateSaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateSaleButton.ForeColor = System.Drawing.Color.White;
            this.CreateSaleButton.Location = new System.Drawing.Point(650, 40);
            this.CreateSaleButton.Name = "CreateSaleButton";
            this.CreateSaleButton.Size = new System.Drawing.Size(105, 30);
            this.CreateSaleButton.TabIndex = 0;
            this.CreateSaleButton.Text = "Create sale";
            this.CreateSaleButton.UseVisualStyleBackColor = false;
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
            this.LanugageMenuItem});
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
            this.LanugageMenuItem.Size = new System.Drawing.Size(126, 22);
            this.LanugageMenuItem.Text = "Language";
            // 
            // RoLangMenuItem
            // 
            this.RoLangMenuItem.Name = "RoLangMenuItem";
            this.RoLangMenuItem.Size = new System.Drawing.Size(140, 22);
            this.RoLangMenuItem.Text = "Română (ro)";
            // 
            // EnLangMenuItem
            // 
            this.EnLangMenuItem.Checked = true;
            this.EnLangMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnLangMenuItem.Name = "EnLangMenuItem";
            this.EnLangMenuItem.Size = new System.Drawing.Size(140, 22);
            this.EnLangMenuItem.Text = "English (en)";
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
            // 
            // AddProductButton
            // 
            this.AddProductButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddProductButton.AutoSize = true;
            this.AddProductButton.BackColor = System.Drawing.Color.Green;
            this.AddProductButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddProductButton.ForeColor = System.Drawing.Color.White;
            this.AddProductButton.Location = new System.Drawing.Point(767, 40);
            this.AddProductButton.Name = "AddProductButton";
            this.AddProductButton.Size = new System.Drawing.Size(105, 30);
            this.AddProductButton.TabIndex = 2;
            this.AddProductButton.Text = "Add product";
            this.AddProductButton.UseVisualStyleBackColor = false;
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
            this.ProductsTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductsTable.ColumnHeadersHeight = 30;
            this.ProductsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductDisplayName,
            this.ProductDescription,
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
            this.ProductsTable.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.ProductsTable.Size = new System.Drawing.Size(876, 395);
            this.ProductsTable.TabIndex = 4;
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
            this.TabControl.Location = new System.Drawing.Point(0, 89);
            this.TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(884, 424);
            this.TabControl.TabIndex = 5;
            // 
            // ProductsPage
            // 
            this.ProductsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ProductsPage.Controls.Add(this.ProductsTable);
            this.ProductsPage.Location = new System.Drawing.Point(4, 25);
            this.ProductsPage.Margin = new System.Windows.Forms.Padding(0);
            this.ProductsPage.Name = "ProductsPage";
            this.ProductsPage.Size = new System.Drawing.Size(876, 395);
            this.ProductsPage.TabIndex = 0;
            this.ProductsPage.Text = "Products";
            // 
            // SalesPage
            // 
            this.SalesPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.SalesPage.Controls.Add(this.SalesTable);
            this.SalesPage.Location = new System.Drawing.Point(4, 25);
            this.SalesPage.Margin = new System.Windows.Forms.Padding(0);
            this.SalesPage.Name = "SalesPage";
            this.SalesPage.Size = new System.Drawing.Size(876, 408);
            this.SalesPage.TabIndex = 1;
            this.SalesPage.Text = "Sales";
            // 
            // SalesTable
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.SalesTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.SalesTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SalesTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.SalesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleID,
            this.SoldProduct,
            this.SaleQuantity,
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
            this.SalesTable.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.SalesTable.Size = new System.Drawing.Size(876, 408);
            this.SalesTable.TabIndex = 5;
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
            this.SoldProduct.Width = 80;
            // 
            // SaleQuantity
            // 
            this.SaleQuantity.HeaderText = "Quantity";
            this.SaleQuantity.MinimumWidth = 20;
            this.SaleQuantity.Name = "SaleQuantity";
            this.SaleQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SaleQuantity.Width = 90;
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
            // ProductCategoriesPage
            // 
            this.ProductCategoriesPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ProductCategoriesPage.Location = new System.Drawing.Point(4, 25);
            this.ProductCategoriesPage.Margin = new System.Windows.Forms.Padding(0);
            this.ProductCategoriesPage.Name = "ProductCategoriesPage";
            this.ProductCategoriesPage.Size = new System.Drawing.Size(876, 408);
            this.ProductCategoriesPage.TabIndex = 2;
            this.ProductCategoriesPage.Text = "Product categories";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.NumberOfProductsLabel);
            this.Controls.Add(this.AddProductButton);
            this.Controls.Add(this.AppMenuBar);
            this.Controls.Add(this.CreateSaleButton);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.AppMenuBar;
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "MainForm";
            this.Text = "Shop Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.AppMenuBar.ResumeLayout(false);
            this.AppMenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsTable)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.ProductsPage.ResumeLayout(false);
            this.SalesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SalesTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateSaleButton;
        private System.Windows.Forms.MenuStrip AppMenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.Button AddProductButton;
        private System.Windows.Forms.ToolStripMenuItem LanugageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RoLangMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnLangMenuItem;
        private System.Windows.Forms.Label NumberOfProductsLabel;
        private System.Windows.Forms.DataGridView ProductsTable;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage ProductsPage;
        private System.Windows.Forms.TabPage SalesPage;
        private System.Windows.Forms.DataGridView SalesTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleQuantity;
        private System.Windows.Forms.DataGridViewButtonColumn SaleDeleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductDisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPurchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductQuantity;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProductCategoryName;
        private System.Windows.Forms.DataGridViewButtonColumn ProductDeleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spacer;
        private System.Windows.Forms.TabPage ProductCategoriesPage;
    }
}

