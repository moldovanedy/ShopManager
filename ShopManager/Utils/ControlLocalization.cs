using System.Windows.Forms;
using ShopManager.Resources.Locale;

namespace ShopManager.Utils
{
    internal static class ControlLocalization
    {
        internal static void Translate(Control parentControl)
        {
            parentControl.Text =
                Strings.ResourceManager.GetString(parentControl.Text) ?? parentControl.Text;
            foreach (Control child in parentControl.Controls)
            {
                child.Text = Strings.ResourceManager.GetString(child.Text) ?? child.Text;
                Translate(child);
            }
        }
    }
}
