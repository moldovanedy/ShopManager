using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Controller.CacheManager;
using ShopManager.Controller.DataModels;
using ShopManager.Controller.ResultHandler;
using ShopManager.Resources.Locale;

namespace ShopManager.AccountManagement
{
    public partial class AdminControlPage : UserControl, IUserForm
    {
        public AdminControlPage()
        {
            InitializeComponent();
            Translate();

            this.RoleComboBox.SelectedIndex = 0;
            UsersListBox_SelectedIndexChanged(null, null);
            RegenerateUserList();
        }

        public void Translate()
        {
            this.ManageUsersLabel.Text = Strings.Manage_users;
            this.DeleteUsersButton.Text = Strings.Delete_selected;
            this.DeselectButton.Text = Strings.Deselect;
            this.ModifyUserDataLabel.Text = Strings.Modify_user_data_;
            this.UsernameLabel.Text = Strings.Username_;
            this.PasswordLabel.Text = Strings.Password_;
            this.RoleLabel.Text = Strings.Role;
            this.ModifyButton.Text = Strings.Modify;
            this.SaveWarningLabel.Text = Strings.Any_modification_will_be_applied_immediately___;

            this.RoleComboBox.Items.Add(Strings.User);
            this.RoleComboBox.Items.Add(Strings.Admin);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AccountManagementWindow wnd = (AccountManagementWindow)this.Parent;
            wnd.Controls.Remove(this);
            wnd.Controls.Add(new AccountDashboardPage());
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (this.UsersListBox.SelectedItems.Count == 0)
            {
                AddUser();
            }
            else
            {
                ModifyUser();
            }
        }

        private void UsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UsernameTextBox.Enabled = true;
            this.PasswordTextBox.Enabled = true;
            this.RoleComboBox.Enabled = true;
            this.ModifyButton.Enabled = true;

            if (this.UsersListBox.SelectedItems.Count == 0)
            {
                this.ModifyUserDataLabel.Text = Strings.Add_new_user_;
                this.ModifyButton.Text = Strings.Add;
                this.DeleteUsersButton.Enabled = false;

                this.UsernameTextBox.Text = "";
                this.PasswordTextBox.Text = "";
                this.RoleComboBox.SelectedIndex = 0;
            }
            else if (this.UsersListBox.SelectedItems.Count == 1)
            {
                this.ModifyUserDataLabel.Text = Strings.Modify_user_data_;
                this.ModifyButton.Text = Strings.Modify;
                this.DeleteUsersButton.Enabled = true;

                ValueResult<User> userResult =
                    UsersCache.SearchSingleUser(this.UsersListBox.SelectedItems[0].ToString());
                if (!userResult.IsSuccess)
                {
                    return;
                }

                this.UsernameTextBox.Text = userResult.Value.Username;
                this.RoleComboBox.SelectedIndex = userResult.Value.Permissions != 0 ? 1 : 0;
            }
            else
            {
                this.ModifyUserDataLabel.Text = Strings.Add_new_user_;
                this.ModifyButton.Text = Strings.Modify;
                this.DeleteUsersButton.Enabled = true;

                this.UsernameTextBox.Enabled = false;
                this.PasswordTextBox.Enabled = false;
                this.RoleComboBox.Enabled = false;
                this.ModifyButton.Enabled = false;
            }
        }

        private void DeselectButton_Click(object sender, EventArgs e)
        {
            this.UsersListBox.SelectedIndices.Clear();
        }

        private async void DeleteUsersButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = this.UsersListBox.SelectedItems;
            foreach (object item in selectedItems)
            {
                ValueResult<User> valueResult = UsersCache.SearchSingleUser(item.ToString());
                if (!valueResult.IsSuccess)
                {
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }

                if (valueResult.Value.ID == 1)
                {
                    MessageBox.Show(
                        string.Format(Messages.CANNOT_DELETE_ADMIN, valueResult.Value.Username),
                        Messages.VALIDATION_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    continue;
                }

                Result deleteResult = UsersCache.DeleteUser(valueResult.Value.ID);
                if (!deleteResult.IsSuccess)
                {
                    MessageBox.Show(
                        Messages.UNEXPECTED_ERROR_TEXT,
                        Messages.UNEXPECTED_ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
            }

            Result saveResult = await UsersCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            RegenerateUserList();
            this.UsersListBox.SelectedIndex = 0;
            this.UsersListBox.SelectedIndices.Clear();
        }


        private async void AddUser()
        {
            bool isValid = ValidateData();
            if (!isValid)
            {
                return;
            }

            //check for duplicate usernames
            if (UsersCache.SearchSingleUser(this.UsernameTextBox.Text).IsSuccess)
            {
                MessageBox.Show(
                    Messages.VALIDATION_USERNAME_EXISTS,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            SHA256 cryptoProvider = SHA256.Create();
            byte[] hashedPasswordRaw =
                cryptoProvider.ComputeHash(
                    Encoding.UTF8.GetBytes(this.PasswordTextBox.Text));

            User user = new User()
            {
                Username = this.UsernameTextBox.Text,
                Password = BitConverter.ToString(hashedPasswordRaw).Replace("-", ""),
                Permissions = (byte)(this.RoleComboBox.SelectedIndex == 0 ? 0 : 1),
            };
            Result addResult = UsersCache.AddUser(user);
            if (!addResult.IsSuccess)
            {
                Logger.LogError(addResult.ResultingError.Description);
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
                Logger.LogError(saveResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Cleanup();
            RegenerateUserList();
        }

        private async void ModifyUser()
        {
            string previousName = this.UsersListBox.SelectedItems[0].ToString();
            ValueResult<User> searchResult = UsersCache.SearchSingleUser(previousName);
            if (!searchResult.IsSuccess)
            {
                Logger.LogError(searchResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            bool isValid = ValidateData();
            if (!isValid)
            {
                return;
            }

            SHA256 cryptoProvider = SHA256.Create();
            byte[] hashedPasswordRaw =
                cryptoProvider.ComputeHash(
                    Encoding.UTF8.GetBytes(this.PasswordTextBox.Text));

            User user = new User()
            {
                ID = searchResult.Value.ID,
                Username = this.UsernameTextBox.Text,
                Password = BitConverter.ToString(hashedPasswordRaw).Replace("-", ""),
                Permissions = (byte)(this.RoleComboBox.SelectedIndex == 0 ? 0 : 1),
            };
            Result updateResult = UsersCache.UpdateUser(user);
            if (!updateResult.IsSuccess)
            {
                Logger.LogError(updateResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Result saveResult = await UsersCache.FlushCacheToDBAsync();
            if (!saveResult.IsSuccess)
            {
                Logger.LogError(saveResult.ResultingError.Description);
                MessageBox.Show(
                    Messages.UNEXPECTED_ERROR_TEXT,
                    Messages.UNEXPECTED_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            //update the cache
            if (user.ID == CredentialsMemoryStore.CurrentUser.ID)
            {
                CredentialsMemoryStore.CurrentUser = user;
            }

            Cleanup();
            RegenerateUserList();
        }

        private bool ValidateData()
        {
            if (this.UsernameTextBox.Text.Length == 0)
            {
                MessageBox.Show(
                    Messages.VALIDATION_USERNAME_SHORT,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (this.UsernameTextBox.Text.Length > 255)
            {
                MessageBox.Show(
                    Messages.VALIDATION_USERNAME_LONG,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (this.PasswordTextBox.Text.Length < 3)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_SHORT,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (this.PasswordTextBox.Text.Length > 255)
            {
                MessageBox.Show(
                    Messages.VALIDATION_PASSWORD_LONG,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (this.RoleComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(
                    Messages.VALIDATION_ROLE_INVALID,
                    Messages.VALIDATION_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void Cleanup()
        {
            this.UsernameTextBox.Text = "";
            this.PasswordTextBox.Text = "";
            this.RoleComboBox.SelectedIndex = 0;

            this.UsersListBox.SelectedIndices.Clear();
        }

        private void RegenerateUserList()
        {
            this.UsersListBox.Items.Clear();

            List<User> users = UsersCache.GetAllUsers();
            foreach (User user in users)
            {
                this.UsersListBox.Items.Add(user.Username);
            }
        }
    }
}
