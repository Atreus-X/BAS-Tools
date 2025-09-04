using System;
using System.Windows.Forms;

namespace BAS_Tools.Licensing
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
            hardwareIdTextBox.Text = LicenseManager.GetHardwareId();
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            if (LicenseManager.ValidateLicense(licenseKeyTextBox.Text))
            {
                LicenseManager.SaveLicense(licenseKeyTextBox.Text);
                MessageBox.Show("Activation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid license key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(hardwareIdTextBox.Text);
            MessageBox.Show("Hardware ID copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

