using System;
using System.Windows.Forms;
using ShopManager.Resources.Locale;

namespace ShopManager.AccountManagement
{
    public partial class AccountDashboardPage : UserControl, IUserForm
    {
        public AccountDashboardPage()
        {
            InitializeComponent();
            Translate();
        }

        public void Translate()
        {
            this.ChangeUsernameButton.Text = Strings.Change_username;
            this.ChangePasswordButton.Text = Strings.Change_password;
            this.AdminButton.Text = Strings.ADMIN__Manage_users;
            this.ChangeButton.Text = Strings.Modify;
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            AccountManagementWindow wnd = (AccountManagementWindow)this.Parent;
            wnd.Controls.Remove(this);
            wnd.Controls.Add(new AdminControlPage());
        }

        private void ChangeUsernameButton_Click(object sender, EventArgs e)
        {
            this.ChangeUsernameButton.Enabled = false;
            this.ChangePasswordButton.Enabled = true;

            ToggleSecondControls(false);
            ToggleFirstControls(true);
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            this.ChangeUsernameButton.Enabled = true;
            this.ChangePasswordButton.Enabled = false;

            ToggleFirstControls(true);
            ToggleSecondControls(true);
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            this.ChangeUsernameButton.Enabled = true;
            this.ChangePasswordButton.Enabled = true;
            ToggleFirstControls(false);
            ToggleSecondControls(false);
        }

        private void ToggleFirstControls(bool enable)
        {
            this.ChangeFirstLabel.Enabled = enable;
            this.ChangeFirstLabel.Visible = enable;

            this.ChangeFirstTextBox.Enabled = enable;
            this.ChangeFirstTextBox.Visible = enable;

            this.ChangeButton.Enabled = enable;
            this.ChangeButton.Visible = enable;

            this.ChangeFirstLabel.Text = Strings.New_username_;
        }

        private void ToggleSecondControls(bool enable)
        {
            this.ChangeSecondLabel.Enabled = enable;
            this.ChangeSecondLabel.Visible = enable;

            this.ChangeSecondTextBox.Enabled = enable;
            this.ChangeSecondTextBox.Visible = enable;

            this.ChangeButton.Enabled = enable;
            this.ChangeButton.Visible = enable;

            this.ChangeFirstLabel.Text = Strings.Password_;
            this.ChangeSecondLabel.Text = Strings.New_password_;
        }
    }
}
