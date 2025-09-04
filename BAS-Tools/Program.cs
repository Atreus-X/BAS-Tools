using MainApp.Licensing;
using System;
using System.Windows.Forms;

namespace BAS_Tools
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (LicenseManager.IsLicensed())
            {
                Application.Run(new MainApp.MainApp());
            }
            else
            {
                using (var licenseForm = new LicenseForm())
                {
                    if (licenseForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new MainApp.MainApp());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }
    }
}
