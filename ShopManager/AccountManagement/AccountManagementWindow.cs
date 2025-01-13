using System.Windows.Forms;
using ShopManager.Resources.Locale;

namespace ShopManager.AccountManagement
{
    public partial class AccountManagementWindow : Form
    {
        public AccountManagementWindow()
        {
            InitializeComponent();
            this.Text = Strings.Account_management;
        }
    }
}
