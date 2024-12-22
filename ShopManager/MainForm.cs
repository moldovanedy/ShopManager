using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ShopManager.Controller.DBManager;
using ShopManager.Extensions;
using ShopManager.Resources.Locale;
using ShopManager.Utils;

namespace ShopManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ro");

            Translate();
            this.AppMenuBar.Renderer = new CustomMenuBarRenderer();
            this.AppMenuBar.ForeColor = Color.White;

            SetupMenu(this.FileMenuItem);
            SetupMenu(this.HelpMenuItem);
        }

        /// <summary>
        /// Sets the text color to white and translations for a whole top-level menu recursively.
        /// </summary>
        /// <param name="higherLevelMenu"></param>
        private void SetupMenu(ToolStripMenuItem higherLevelMenu)
        {
            foreach (object subMenu in higherLevelMenu.DropDownItems)
            {
                ToolStripMenuItem menu = (ToolStripMenuItem)subMenu;
                menu.ForeColor = Color.White;
                menu.Text = Strings.ResourceManager.GetString(menu.Text) ?? menu.Text;
                SetupMenu(menu);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _ = MasterDBController.InitializeDBAsync();
        }

        private void Translate()
        {
            ControlLocalization.Translate(this);
            this.FileMenuItem.Text = Strings.File;
            this.HelpMenuItem.Text = Strings.Help;
        }
    }
}
