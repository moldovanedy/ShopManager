namespace ShopManager
{
    partial class CreateSaleWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSaleWindow));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ProductLabel = new System.Windows.Forms.Label();
            this.ProductDropDown = new System.Windows.Forms.ComboBox();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.DebounceTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            resources.ApplyResources(this.TitleLabel, "TitleLabel");
            this.TitleLabel.Name = "TitleLabel";
            // 
            // ProductLabel
            // 
            resources.ApplyResources(this.ProductLabel, "ProductLabel");
            this.ProductLabel.Name = "ProductLabel";
            // 
            // ProductDropDown
            // 
            resources.ApplyResources(this.ProductDropDown, "ProductDropDown");
            this.ProductDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ProductDropDown.ForeColor = System.Drawing.Color.White;
            this.ProductDropDown.FormattingEnabled = true;
            this.ProductDropDown.Name = "ProductDropDown";
            this.ProductDropDown.SelectedIndexChanged += new System.EventHandler(this.ProductDropDown_SelectedIndexChanged);
            this.ProductDropDown.TextUpdate += new System.EventHandler(this.ProductDropDown_TextUpdate);
            this.ProductDropDown.TextChanged += new System.EventHandler(this.ProductDropDown_TextChanged);
            // 
            // QuantityLabel
            // 
            resources.ApplyResources(this.QuantityLabel, "QuantityLabel");
            this.QuantityLabel.Name = "QuantityLabel";
            // 
            // QuantityTextBox
            // 
            resources.ApplyResources(this.QuantityTextBox, "QuantityTextBox");
            this.QuantityTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.QuantityTextBox.ForeColor = System.Drawing.Color.White;
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuantityTextBox_KeyDown);
            // 
            // CancelButton
            // 
            resources.ApplyResources(this.CancelButton, "CancelButton");
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CreateButton
            // 
            resources.ApplyResources(this.CreateButton, "CreateButton");
            this.CreateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // DebounceTimer
            // 
            this.DebounceTimer.Interval = 1500;
            // 
            // CreateSaleWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.QuantityTextBox);
            this.Controls.Add(this.QuantityLabel);
            this.Controls.Add(this.ProductDropDown);
            this.Controls.Add(this.ProductLabel);
            this.Controls.Add(this.TitleLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CreateSaleWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.CreateSaleWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label ProductLabel;
        private System.Windows.Forms.ComboBox ProductDropDown;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Timer DebounceTimer;
    }
}