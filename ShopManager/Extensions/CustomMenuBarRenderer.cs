using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.Extensions
{
    /// <summary>
    /// A custom renderer for the menu bar because we don't like the default white+blue one.
    /// </summary>
    /// <remarks>
    /// Credit to https://stackoverflow.com/a/71956306.
    /// </remarks>
    internal class CustomMenuBarRenderer : ToolStripProfessionalRenderer
    {
        internal CustomMenuBarRenderer() : base(new MenuBarColorTable()) { }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripMenuItem)
            {
                e.ArrowColor = Color.White;
            }
            base.OnRenderArrow(e);
        }

        private class MenuBarColorTable : ProfessionalColorTable
        {
            //the actual menu bar
            public override Color MenuStripGradientBegin => Color.FromArgb(0xff, 0x61, 0x61, 0x61);
            public override Color MenuStripGradientEnd => Color.FromArgb(0xff, 0x61, 0x61, 0x61);


            //the bg of top-level menu on hover
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(0xff, 0x80, 0x80, 0x80);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(0xff, 0x80, 0x80, 0x80);

            //the bg of top-level menu when active
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(0xff, 0x0d, 0x47, 0xa1);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(0xff, 0x0d, 0x47, 0xa1);

            //the border of top-level menu on hover
            public override Color MenuItemBorder => Color.Transparent;


            //the bg of sub-menu when active or hover
            public override Color MenuItemSelected => Color.FromArgb(0xff, 0x0d, 0x47, 0xa1);

            //the bg of the sub-menus
            public override Color ToolStripDropDownBackground => Color.FromArgb(0xff, 0x61, 0x61, 0x61);

            //the bg of the right margin of the sub-menus
            public override Color ImageMarginGradientBegin => Color.FromArgb(0xff, 0x61, 0x61, 0x61);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(0xff, 0x61, 0x61, 0x61);
            public override Color ImageMarginGradientEnd => Color.FromArgb(0xff, 0x61, 0x61, 0x61);

            //the border of the sub-menus
            public override Color MenuBorder => Color.FromArgb(0xff, 0xbd, 0xbd, 0xbd);


            //the color of the separator?
            public override Color SeparatorDark => Color.White;
        }
    }
}
