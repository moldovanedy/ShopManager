﻿using System;
using System.Windows.Forms;
using ShopManager.Controller;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    Messages.FATAL_ERROR_TEXT,
                    Messages.FATAL_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                Logger.LogError($"Unhandled exception at top-level: {ex.Message}, {ex.StackTrace}");
            }
            finally
            {
                Logger.Flush();
            }
        }
    }
}
