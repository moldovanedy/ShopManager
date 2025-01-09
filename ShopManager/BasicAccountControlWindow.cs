using System.Windows.Forms;
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
            this.TitleLabel.Text = Strings.Log_in_to_the_shop_manager;
            this.UsernameLabel.Text = Strings.Username_;
            this.PasswordLabel.Text = _isInForgotPasswordMode ? Strings.New_password_ : Strings.Password_;
            this.RegisterExplanationLabel.Text = Strings.REGISTER_EXPLANATION;
            this.ForgotPasswordLink.Text = _isInForgotPasswordMode ? Strings.Login : Strings.Forgot_password;
            this.LoginButton.Text = Strings.Login;
        }

        private void ForgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _isInForgotPasswordMode = !_isInForgotPasswordMode;
            Translate();

            this.RegisterExplanationLabel.Visible = !_isInForgotPasswordMode;
            this.PasswordTextBox.Text = "";
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            Login();
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

            Login();
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

            DialogResult = DialogResult.OK;
        }
    }
}
