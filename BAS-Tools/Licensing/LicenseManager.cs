using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BAS_Tools.Licensing
{
    public static class LicenseManager
    {
        private static readonly string LicenseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "license.key");

        public static string GetHardwareId()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (var o in searcher.Get())
                    {
                        var obj = (ManagementObject)o;
                        return obj["ProcessorId"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not retrieve hardware ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "unknown";
        }

        public static bool ValidateLicense(string licenseKey)
        {
            string hardwareId = GetHardwareId();
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hardwareId + LicenseSecret.SecretKey));
                string expectedKey = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return licenseKey.Equals(expectedKey, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool IsLicenseValid()
        {
            if (!File.Exists(LicenseFile))
                return false;

            string licenseKey = File.ReadAllText(LicenseFile);
            return ValidateLicense(licenseKey);
        }

        public static void SaveLicense(string licenseKey)
        {
            try
            {
                File.WriteAllText(LicenseFile, licenseKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save license file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

