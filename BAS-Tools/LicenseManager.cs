using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MainApp.Licensing
{
    public static class LicenseManager
    {
        private const string SECRET_SALT = "tXA9/Cxbp7lSJ66Hwd5gGMGd5QorvhG1T5jOCgAx6Z0=";
        private static readonly string LicenseFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "BAS-Tools",
            "license.key");

        public static string GetHardwareId()
        {
            try
            {
                var macAddr =
                (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();

                if (string.IsNullOrEmpty(macAddr))
                {
                    return "No-Active-NIC";
                }

                return macAddr;
            }
            catch
            {
                return "Error-HWID";
            }
        }

        internal static string GenerateLicenseKey(string hardwareId)
        {
            if (string.IsNullOrEmpty(hardwareId)) return "";

            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedId = hardwareId.ToUpper() + SECRET_SALT;
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedId));
                string base64Hash = Convert.ToBase64String(hash).ToUpper().Replace("=", "");

                string key = base64Hash.Substring(0, 24);

                // Format as XXXX-XXXX-XXXX-XXXX-XXXX-XXXX
                return string.Join("-", Enumerable.Range(0, key.Length / 4)
                                                  .Select(i => key.Substring(i * 4, 4)));
            }
        }

        public static bool ValidateLicense(string licenseKey)
        {
            if (string.IsNullOrEmpty(licenseKey)) return false;

            string hardwareId = GetHardwareId();
            string expectedKey = GenerateLicenseKey(hardwareId);

            return string.Equals(licenseKey.Replace("-", ""), expectedKey.Replace("-", ""), StringComparison.OrdinalIgnoreCase);
        }

        public static void SaveLicense(string licenseKey)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LicenseFilePath));
                File.WriteAllText(LicenseFilePath, licenseKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save license file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string LoadLicense()
        {
            try
            {
                if (File.Exists(LicenseFilePath))
                {
                    return File.ReadAllText(LicenseFilePath).Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load license file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static bool IsLicensed()
        {
            string storedLicense = LoadLicense();
            if (string.IsNullOrEmpty(storedLicense))
            {
                return false;
            }
            return ValidateLicense(storedLicense);
        }
    }
}
