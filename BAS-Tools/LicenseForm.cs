using System;
using System.Windows.Forms;

namespace MainApp.Licensing
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
            txtHardwareId.Text = LicenseManager.GetHardwareId();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string key = txtLicenseKey.Text.Trim();
            if (LicenseManager.ValidateLicense(key))
            {
                LicenseManager.SaveLicense(key);
                MessageBox.Show("Application activated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("The license key is invalid for this hardware.", "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
