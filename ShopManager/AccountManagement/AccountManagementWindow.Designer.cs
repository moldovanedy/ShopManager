namespace ShopManager.AccountManagement
{
    partial class AccountManagementWindow
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
            this.AccountDashboardPage = new ShopManager.AccountManagement.AccountDashboardPage();
            this.SuspendLayout();
            // 
            // AccountDashboardPage
            // 
            this.AccountDashboardPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.AccountDashboardPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountDashboardPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.AccountDashboardPage.ForeColor = System.Drawing.Color.White;
            this.AccountDashboardPage.Location = new System.Drawing.Point(0, 0);
            this.AccountDashboardPage.Margin = new System.Windows.Forms.Padding(4);
            this.AccountDashboardPage.Name = "AccountDashboardPage";
            this.AccountDashboardPage.Size = new System.Drawing.Size(484, 451);
            this.AccountDashboardPage.TabIndex = 0;
            // 
            // AccountManagementWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(484, 451);
            this.Controls.Add(this.AccountDashboardPage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 490);
            this.Name = "AccountManagementWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account management";
            this.ResumeLayout(false);

        }

        #endregion

        private AccountDashboardPage AccountDashboardPage;
    }
}