using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.DataModels;
using ShopManager.Controller.ResultHandler;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    public partial class BasicAccountControlWindow : Form, IUserForm
    {
        private bool _isInForgotPasswordMode = false;

        public BasicAccountControlWindow()
        {
            InitializeComponent();
            Translate();
            DialogResult = DialogResult.Cancel;
        }

        public void Translate()
        {
            this.Text = Strings.Login;
            this.TitleLabel.Text = Strings.Log_in_to_the_shop_manager;
            this.UsernameLabel.Text = Strings.Username_;
            this.PasswordLabel.Text = _isInForgotPasswordMode ? Strings.New_password_ : Strings.Password_;
            this.AdminPasswordLabel.Text = Strings.Admin_password_;
            this.AdminUsernameLabel.Text = Strings.Admin_username_;
            this.RegisterExplanationLabel.Text = Strings.REGISTER_EXPLANATION;
            this.ForgotPasswordLink.Text = _isInForgotPasswordMode ? Strings.Login : Strings.Forgot_password;
            this.LoginButton.Text = Strings.Login;
        }

        private void ForgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _isInForgotPasswordMode = !_isInForgotPasswordMode;
            Translate();

            this.AdminUsernameLabel.Enabled = _isInForgotPasswordMode;
            this.AdminUsernameLabel.Visible = _isInForgotPasswordMode;
            this.AdminUsernameTextBox.Enabled = _isInForgotPasswordMode;
            this.AdminUsernameTextBox.Visible = _isInForgotPasswordMode;

            this.AdminPasswordLabel.Enabled = _isInForgotPasswordMode;
            this.AdminPasswordLabel.Visible = _isInForgotPasswordMode;
            this.AdminPasswordTextBox.Enabled = _isInForgotPasswordMode;
            this.AdminPasswordTextBox.Visible = _isInForgotPasswordMode;

            this.RegisterExplanationLabel.Visible = !_isInForgotPasswordMode;
            this.PasswordTextBox.Text = "";
            this.AdminPasswordTextBox.Text = "";
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            if (_isInForgotPasswordMode)
            {
                ResetPasswordAndLogin();
            }
            else
            {
                Login();
            }
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            this.PasswordTextBox.Focus();
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (!_isInForgotPasswordMode)
            {
                Login();
            }
        }

        private void AdminPasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            ResetPasswordAndLogin();
        }


        private void Login()
        {
            if (string.IsNullOrEmpty(this.UsernameTextBox.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.PasswordTextBox.Text))
            {
                return;
            }

            if (this.UsernameTextBox.Text.Length > 255)
            {
                MessageBox.Show(
                    Messages.VALIDATION_USERNAME_LONG,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (this.PasswordTextBox.Text.Length < 3)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_SHORT,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (this.PasswordTextBox.Text.Length > 255)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_LONG,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ValueResult<User> userResult = UsersCache.SearchSingleUser(this.UsernameTextBox.Text);
            if (!userResult.IsSuccess)
            {
                MessageBox.Show(
                    Messages.LOGIN_INVALID_CREDENTIALS,
                    Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SHA256 cryptoProvider = SHA256.Create();
            byte[] rawHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(this.PasswordTextBox.Text));
            string hash = BitConverter.ToString(rawHash).Replace("-", "");

            if (!userResult.Value.Password.Equals(hash, StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show(
                    Messages.LOGIN_INVALID_CREDENTIALS,
                    Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            CredentialsMemoryStore.CurrentUser = userResult.Value;
            DialogResult = DialogResult.OK;
        }

        private async void ResetPasswordAndLogin()
        {
            if (string.IsNullOrEmpty(this.UsernameTextBox.Text))
            {
                return;
            }
            if (this.PasswordTextBox.Text == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.AdminUsernameTextBox.Text))
            {
                return;
            }
            if (this.AdminPasswordTextBox.Text == null)
            {
                return;
            }

            if (this.PasswordTextBox.Text.Length < 3)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_SHORT,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (this.PasswordTextBox.Text.Length > 255)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_LONG,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ValueResult<User> adminResult = UsersCache.SearchSingleUser(this.AdminUsernameTextBox.Text);
            if (!adminResult.IsSuccess || adminResult.Value.Permissions == 0)
            {
                MessageBox.Show(
                    Messages.LOGIN_ADMIN_INVALID_CREDENTIALS,
                    Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SHA256 cryptoProvider = SHA256.Create();
            //if it is an admin and it is trying to reset its own password, skip password check
            if (this.AdminUsernameTextBox.Text == this.UsernameTextBox.Text)
            {
                goto ResetPassword;
            }

            byte[] rawHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(this.AdminPasswordTextBox.Text));
            string hash = BitConverter.ToString(rawHash).Replace("-", "");

            if (!adminResult.Value.Password.Equals(hash, StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show(
                    Messages.LOGIN_ADMIN_INVALID_CREDENTIALS,
                    Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

        ResetPassword:
            ValueResult<User> userResult = UsersCache.SearchSingleUser(this.UsernameTextBox.Text);
            if (!userResult.IsSuccess)
            {
                MessageBox.Show(
                    Messages.LOGIN_INVALID_CREDENTIALS,
                    Messages.LOGIN_INVALID_CREDENTIALS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            rawHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(this.PasswordTextBox.Text));
            hash = BitConverter.ToString(rawHash).Replace("-", "");
            userResult.Value.Password = hash;

            Result updateResult = UsersCache.UpdateUser(userResult.Value);
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

            CredentialsMemoryStore.CurrentUser = userResult.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
