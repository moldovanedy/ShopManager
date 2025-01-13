namespace ShopManager.AccountManagement
{
    partial class AdminControlPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminControlPage));
            this.UsersListBox = new System.Windows.Forms.ListBox();
            this.ManageUsersLabel = new System.Windows.Forms.Label();
            this.DeselectButton = new System.Windows.Forms.Button();
            this.DeleteUsersButton = new System.Windows.Forms.Button();
            this.ModifyUserDataLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.RoleLabel = new System.Windows.Forms.Label();
            this.RoleComboBox = new System.Windows.Forms.ComboBox();
            this.ModifyButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.SaveWarningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UsersListBox
            // 
            this.UsersListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.UsersListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UsersListBox.ForeColor = System.Drawing.Color.White;
            this.UsersListBox.FormattingEnabled = true;
            this.UsersListBox.ItemHeight = 16;
            this.UsersListBox.Location = new System.Drawing.Point(5, 65);
            this.UsersListBox.Name = "UsersListBox";
            this.UsersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.UsersListBox.Size = new System.Drawing.Size(200, 176);
            this.UsersListBox.TabIndex = 0;
            this.UsersListBox.SelectedIndexChanged += new System.EventHandler(this.UsersListBox_SelectedIndexChanged);
            // 
            // ManageUsersLabel
            // 
            this.ManageUsersLabel.AutoSize = true;
            this.ManageUsersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.ManageUsersLabel.Location = new System.Drawing.Point(40, 5);
            this.ManageUsersLabel.Name = "ManageUsersLabel";
            this.ManageUsersLabel.Size = new System.Drawing.Size(123, 22);
            this.ManageUsersLabel.TabIndex = 1;
            this.ManageUsersLabel.Text = "Manage users";
            // 
            // DeselectButton
            // 
            this.DeselectButton.AutoSize = true;
            this.DeselectButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.DeselectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeselectButton.ForeColor = System.Drawing.Color.White;
            this.DeselectButton.Location = new System.Drawing.Point(210, 99);
            this.DeselectButton.Name = "DeselectButton";
            this.DeselectButton.Size = new System.Drawing.Size(125, 28);
            this.DeselectButton.TabIndex = 7;
            this.DeselectButton.Text = "Deselect";
            this.DeselectButton.UseVisualStyleBackColor = false;
            this.DeselectButton.Click += new System.EventHandler(this.DeselectButton_Click);
            // 
            // DeleteUsersButton
            // 
            this.DeleteUsersButton.AutoSize = true;
            this.DeleteUsersButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DeleteUsersButton.Enabled = false;
            this.DeleteUsersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteUsersButton.ForeColor = System.Drawing.Color.White;
            this.DeleteUsersButton.Location = new System.Drawing.Point(210, 65);
            this.DeleteUsersButton.Name = "DeleteUsersButton";
            this.DeleteUsersButton.Size = new System.Drawing.Size(125, 28);
            this.DeleteUsersButton.TabIndex = 6;
            this.DeleteUsersButton.Text = "Delete selected";
            this.DeleteUsersButton.UseVisualStyleBackColor = false;
            this.DeleteUsersButton.Click += new System.EventHandler(this.DeleteUsersButton_Click);
            // 
            // ModifyUserDataLabel
            // 
            this.ModifyUserDataLabel.AutoSize = true;
            this.ModifyUserDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ModifyUserDataLabel.Location = new System.Drawing.Point(211, 164);
            this.ModifyUserDataLabel.Name = "ModifyUserDataLabel";
            this.ModifyUserDataLabel.Size = new System.Drawing.Size(130, 20);
            this.ModifyUserDataLabel.TabIndex = 8;
            this.ModifyUserDataLabel.Text = "Modify user data:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(215, 194);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(77, 17);
            this.UsernameLabel.TabIndex = 9;
            this.UsernameLabel.Text = "Username:";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.UsernameTextBox.ForeColor = System.Drawing.Color.White;
            this.UsernameTextBox.Location = new System.Drawing.Point(218, 216);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(200, 23);
            this.UsernameTextBox.TabIndex = 10;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(215, 254);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(73, 17);
            this.PasswordLabel.TabIndex = 11;
            this.PasswordLabel.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.PasswordTextBox.ForeColor = System.Drawing.Color.White;
            this.PasswordTextBox.Location = new System.Drawing.Point(218, 276);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '●';
            this.PasswordTextBox.Size = new System.Drawing.Size(200, 23);
            this.PasswordTextBox.TabIndex = 12;
            // 
            // RoleLabel
            // 
            this.RoleLabel.AutoSize = true;
            this.RoleLabel.Location = new System.Drawing.Point(215, 314);
            this.RoleLabel.Name = "RoleLabel";
            this.RoleLabel.Size = new System.Drawing.Size(41, 17);
            this.RoleLabel.TabIndex = 13;
            this.RoleLabel.Text = "Role:";
            // 
            // RoleComboBox
            // 
            this.RoleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RoleComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.RoleComboBox.ForeColor = System.Drawing.Color.White;
            this.RoleComboBox.FormattingEnabled = true;
            this.RoleComboBox.Location = new System.Drawing.Point(218, 336);
            this.RoleComboBox.Name = "RoleComboBox";
            this.RoleComboBox.Size = new System.Drawing.Size(121, 24);
            this.RoleComboBox.TabIndex = 14;
            // 
            // ModifyButton
            // 
            this.ModifyButton.AutoSize = true;
            this.ModifyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.ModifyButton.Location = new System.Drawing.Point(218, 382);
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(75, 27);
            this.ModifyButton.TabIndex = 15;
            this.ModifyButton.Text = "Modify";
            this.ModifyButton.UseVisualStyleBackColor = false;
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Image = ((System.Drawing.Image)(resources.GetObject("BackButton.Image")));
            this.BackButton.Location = new System.Drawing.Point(5, 5);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(25, 25);
            this.BackButton.TabIndex = 16;
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // SaveWarningLabel
            // 
            this.SaveWarningLabel.AutoSize = true;
            this.SaveWarningLabel.ForeColor = System.Drawing.Color.Salmon;
            this.SaveWarningLabel.Location = new System.Drawing.Point(44, 31);
            this.SaveWarningLabel.Name = "SaveWarningLabel";
            this.SaveWarningLabel.Size = new System.Drawing.Size(290, 17);
            this.SaveWarningLabel.TabIndex = 17;
            this.SaveWarningLabel.Text = "Any modification will be applied immediately!!!";
            // 
            // AdminControlPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.SaveWarningLabel);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.ModifyButton);
            this.Controls.Add(this.RoleComboBox);
            this.Controls.Add(this.RoleLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.ModifyUserDataLabel);
            this.Controls.Add(this.DeselectButton);
            this.Controls.Add(this.DeleteUsersButton);
            this.Controls.Add(this.ManageUsersLabel);
            this.Controls.Add(this.UsersListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminControlPage";
            this.Size = new System.Drawing.Size(450, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox UsersListBox;
        private System.Windows.Forms.Label ManageUsersLabel;
        private System.Windows.Forms.Button DeselectButton;
        private System.Windows.Forms.Button DeleteUsersButton;
        private System.Windows.Forms.Label ModifyUserDataLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label RoleLabel;
        private System.Windows.Forms.ComboBox RoleComboBox;
        private System.Windows.Forms.Button ModifyButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Label SaveWarningLabel;
    }
}
