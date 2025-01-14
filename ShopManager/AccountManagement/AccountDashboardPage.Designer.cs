namespace ShopManager.AccountManagement
{
    partial class AccountDashboardPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountDashboardPage));
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.RoleLabel = new System.Windows.Forms.Label();
            this.ChangeUsernameButton = new System.Windows.Forms.Button();
            this.ChangePasswordButton = new System.Windows.Forms.Button();
            this.ChangeFirstLabel = new System.Windows.Forms.Label();
            this.ChangeFirstTextBox = new System.Windows.Forms.TextBox();
            this.ChangeSecondLabel = new System.Windows.Forms.Label();
            this.ChangeSecondTextBox = new System.Windows.Forms.TextBox();
            this.ModifyButton = new System.Windows.Forms.Button();
            this.AdminButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.UsernameLabel.Location = new System.Drawing.Point(10, 10);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(138, 26);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "USERNAME";
            // 
            // RoleLabel
            // 
            this.RoleLabel.AutoSize = true;
            this.RoleLabel.Location = new System.Drawing.Point(15, 40);
            this.RoleLabel.Name = "RoleLabel";
            this.RoleLabel.Size = new System.Drawing.Size(46, 17);
            this.RoleLabel.TabIndex = 1;
            this.RoleLabel.Text = "ROLE";
            // 
            // ChangeUsernameButton
            // 
            this.ChangeUsernameButton.AutoSize = true;
            this.ChangeUsernameButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChangeUsernameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ChangeUsernameButton.Location = new System.Drawing.Point(15, 80);
            this.ChangeUsernameButton.Name = "ChangeUsernameButton";
            this.ChangeUsernameButton.Size = new System.Drawing.Size(134, 27);
            this.ChangeUsernameButton.TabIndex = 2;
            this.ChangeUsernameButton.Text = "Change username";
            this.ChangeUsernameButton.UseVisualStyleBackColor = false;
            this.ChangeUsernameButton.Click += new System.EventHandler(this.ChangeUsernameButton_Click);
            // 
            // ChangePasswordButton
            // 
            this.ChangePasswordButton.AutoSize = true;
            this.ChangePasswordButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChangePasswordButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ChangePasswordButton.Location = new System.Drawing.Point(15, 115);
            this.ChangePasswordButton.Name = "ChangePasswordButton";
            this.ChangePasswordButton.Size = new System.Drawing.Size(131, 27);
            this.ChangePasswordButton.TabIndex = 3;
            this.ChangePasswordButton.Text = "Change password";
            this.ChangePasswordButton.UseVisualStyleBackColor = false;
            this.ChangePasswordButton.Click += new System.EventHandler(this.ChangePasswordButton_Click);
            // 
            // ChangeFirstLabel
            // 
            this.ChangeFirstLabel.AutoSize = true;
            this.ChangeFirstLabel.Enabled = false;
            this.ChangeFirstLabel.Location = new System.Drawing.Point(15, 165);
            this.ChangeFirstLabel.Name = "ChangeFirstLabel";
            this.ChangeFirstLabel.Size = new System.Drawing.Size(113, 17);
            this.ChangeFirstLabel.TabIndex = 4;
            this.ChangeFirstLabel.Text = "CHANGE FIRST:";
            this.ChangeFirstLabel.Visible = false;
            // 
            // ChangeFirstTextBox
            // 
            this.ChangeFirstTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ChangeFirstTextBox.Enabled = false;
            this.ChangeFirstTextBox.ForeColor = System.Drawing.Color.White;
            this.ChangeFirstTextBox.Location = new System.Drawing.Point(18, 190);
            this.ChangeFirstTextBox.Name = "ChangeFirstTextBox";
            this.ChangeFirstTextBox.Size = new System.Drawing.Size(200, 23);
            this.ChangeFirstTextBox.TabIndex = 5;
            this.ChangeFirstTextBox.Visible = false;
            // 
            // ChangeSecondLabel
            // 
            this.ChangeSecondLabel.AutoSize = true;
            this.ChangeSecondLabel.Enabled = false;
            this.ChangeSecondLabel.Location = new System.Drawing.Point(15, 230);
            this.ChangeSecondLabel.Name = "ChangeSecondLabel";
            this.ChangeSecondLabel.Size = new System.Drawing.Size(132, 17);
            this.ChangeSecondLabel.TabIndex = 6;
            this.ChangeSecondLabel.Text = "CHANGE SECOND:";
            this.ChangeSecondLabel.Visible = false;
            // 
            // ChangeSecondTextBox
            // 
            this.ChangeSecondTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ChangeSecondTextBox.Enabled = false;
            this.ChangeSecondTextBox.ForeColor = System.Drawing.Color.White;
            this.ChangeSecondTextBox.Location = new System.Drawing.Point(18, 255);
            this.ChangeSecondTextBox.Name = "ChangeSecondTextBox";
            this.ChangeSecondTextBox.Size = new System.Drawing.Size(200, 23);
            this.ChangeSecondTextBox.TabIndex = 7;
            this.ChangeSecondTextBox.Visible = false;
            // 
            // ModifyButton
            // 
            this.ModifyButton.AutoSize = true;
            this.ModifyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.ModifyButton.Enabled = false;
            this.ModifyButton.Location = new System.Drawing.Point(18, 305);
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(76, 27);
            this.ModifyButton.TabIndex = 8;
            this.ModifyButton.Text = "Modify";
            this.ModifyButton.UseVisualStyleBackColor = false;
            this.ModifyButton.Visible = false;
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // AdminButton
            // 
            this.AdminButton.AutoSize = true;
            this.AdminButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AdminButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(27)))), ((int)(((byte)(154)))));
            this.AdminButton.Image = ((System.Drawing.Image)(resources.GetObject("AdminButton.Image")));
            this.AdminButton.Location = new System.Drawing.Point(225, 80);
            this.AdminButton.Name = "AdminButton";
            this.AdminButton.Size = new System.Drawing.Size(183, 30);
            this.AdminButton.TabIndex = 9;
            this.AdminButton.Text = "ADMIN: Manage users";
            this.AdminButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AdminButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AdminButton.UseVisualStyleBackColor = false;
            this.AdminButton.Click += new System.EventHandler(this.AdminButton_Click);
            // 
            // AccountDashboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.AdminButton);
            this.Controls.Add(this.ModifyButton);
            this.Controls.Add(this.ChangeSecondTextBox);
            this.Controls.Add(this.ChangeSecondLabel);
            this.Controls.Add(this.ChangeFirstTextBox);
            this.Controls.Add(this.ChangeFirstLabel);
            this.Controls.Add(this.ChangePasswordButton);
            this.Controls.Add(this.ChangeUsernameButton);
            this.Controls.Add(this.RoleLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccountDashboardPage";
            this.Size = new System.Drawing.Size(450, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label RoleLabel;
        private System.Windows.Forms.Button ChangeUsernameButton;
        private System.Windows.Forms.Button ChangePasswordButton;
        private System.Windows.Forms.Label ChangeFirstLabel;
        private System.Windows.Forms.TextBox ChangeFirstTextBox;
        private System.Windows.Forms.Label ChangeSecondLabel;
        private System.Windows.Forms.TextBox ChangeSecondTextBox;
        private System.Windows.Forms.Button ModifyButton;
        private System.Windows.Forms.Button AdminButton;
    }
}
