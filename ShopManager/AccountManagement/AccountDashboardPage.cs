using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.ResultHandler;
using ShopManager.Resources.Locale;

namespace ShopManager.AccountManagement
{
    public partial class AccountDashboardPage : UserControl, IUserForm
    {
        private bool _isChangingPassword = false;

        public AccountDashboardPage()
        {
            InitializeComponent();
            Translate();

            if (CredentialsMemoryStore.CurrentUser == null)
            {
                return;
            }
            if (CredentialsMemoryStore.CurrentUser.Permissions == 0)
            {
                this.AdminButton.Enabled = false;
                this.AdminButton.Visible = false;
            }

            this.UsernameLabel.Text = CredentialsMemoryStore.CurrentUser.Username;
            this.RoleLabel.Text = CredentialsMemoryStore.CurrentUser.Permissions == 0 ? Strings.User : Strings.Admin;
        }

        public void Translate()
        {
            this.ChangeUsernameButton.Text = Strings.Change_username;
            this.ChangePasswordButton.Text = Strings.Change_password;
            this.AdminButton.Text = Strings.ADMIN__Manage_users;
            this.ModifyButton.Text = Strings.Modify;
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

        private async void ModifyButton_Click(object sender, EventArgs e)
        {
            if (_isChangingPassword)
            {
                SHA256 cryptoProvider = SHA256.Create();
                byte[] hashedPasswordRaw;

                hashedPasswordRaw = cryptoProvider.ComputeHash(
                    Encoding.UTF8.GetBytes(this.ChangeFirstTextBox.Text));

                //check the current password
                if (!CredentialsMemoryStore.CurrentUser.Password.Equals(
                        BitConverter.ToString(hashedPasswordRaw).Replace("-", ""),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show(
                        Messages.LOGIN_INVALID_PASSWORD,
                        Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (this.ChangeSecondTextBox.Text.Length < 3)
                {
                    MessageBox.Show(
                        Messages.VALIDATION_PASSWORD_SHORT,
                        Messages.VALIDATION_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (this.ChangeSecondTextBox.Text.Length > 255)
                {
                    MessageBox.Show(
                        Messages.VALIDATION_PASSWORD_LONG,
                        Messages.VALIDATION_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                hashedPasswordRaw =
                    cryptoProvider.ComputeHash(
                        Encoding.UTF8.GetBytes(this.ChangeSecondTextBox.Text));

                CredentialsMemoryStore.CurrentUser.Password =
                    BitConverter.ToString(hashedPasswordRaw).Replace("-", "");
            }
            else
            {
                if (string.IsNullOrEmpty(this.ChangeFirstTextBox.Text))
                {
                    MessageBox.Show(
                        Messages.VALIDATION_USERNAME_SHORT,
                        Messages.VALIDATION_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (this.ChangeFirstTextBox.Text.Length > 255)
                {
                    MessageBox.Show(
                        Messages.VALIDATION_USERNAME_LONG,
                        Messages.VALIDATION_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                CredentialsMemoryStore.CurrentUser.Username = this.ChangeFirstTextBox.Text;
            }

            Result updateResult = UsersCache.UpdateUser(CredentialsMemoryStore.CurrentUser);
            if (!updateResult.IsSuccess)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Result saveResult = await UsersCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.ChangeUsernameButton.Enabled = true;
            this.ChangePasswordButton.Enabled = true;
            ToggleFirstControls(false);
            ToggleSecondControls(false);

            this.UsernameLabel.Text = CredentialsMemoryStore.CurrentUser.Username;
        }

        private void ToggleFirstControls(bool enable)
        {
            //to protect the user accidentally pressing "Modify username" after typing their password in "Modify password",
            //therefore making their password visible
            this.ChangeFirstTextBox.Text = "";

            this.ChangeFirstLabel.Enabled = enable;
            this.ChangeFirstLabel.Visible = enable;

            this.ChangeFirstTextBox.Enabled = enable;
            this.ChangeFirstTextBox.Visible = enable;

            this.ModifyButton.Enabled = enable;
            this.ModifyButton.Visible = enable;

            this.ChangeFirstLabel.Text = Strings.New_username_;
            this.ChangeFirstTextBox.PasswordChar = (char)0;
            this.ChangeSecondTextBox.PasswordChar = (char)0;
        }

        private void ToggleSecondControls(bool enable)
        {
            this.ChangeSecondLabel.Enabled = enable;
            this.ChangeSecondLabel.Visible = enable;

            this.ChangeSecondTextBox.Enabled = enable;
            this.ChangeSecondTextBox.Visible = enable;

            this.ModifyButton.Enabled = enable;
            this.ModifyButton.Visible = enable;

            this.ChangeFirstLabel.Text = Strings.Password_;
            this.ChangeSecondLabel.Text = Strings.New_password_;
            _isChangingPassword = enable;

            this.ChangeFirstTextBox.PasswordChar = '\u25cf';
            this.ChangeSecondTextBox.PasswordChar = '\u25cf';
        }
    }
}
