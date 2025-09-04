using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BAS_Tools.Licensing
{
    internal static class LicenseManager
    {
        private static readonly string LicenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BAS-Tools", "license.key");

        public static bool IsLicensed()
        {
            if (!File.Exists(LicenseFile))
                return false;

            string licenseKey = File.ReadAllText(LicenseFile);
            return ValidateLicense(licenseKey);
        }

        public static bool ValidateLicense(string licenseKey)
        {
            string hardwareId = GetHardwareId();
            string expectedKey = GenerateLicenseKey(hardwareId);
            return licenseKey == expectedKey;
        }

        public static void SaveLicense(string licenseKey)
        {
            string directory = Path.GetDirectoryName(LicenseFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(LicenseFile, licenseKey);
        }

        public static string GetHardwareId()
        {
            try
            {
                string cpuInfo = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject mo in searcher.Get())
                {
                    cpuInfo = mo["ProcessorId"].ToString();
                    break;
                }

                string hddInfo = string.Empty;
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3");
                foreach (ManagementObject mo in searcher.Get())
                {
                    hddInfo = mo["VolumeSerialNumber"].ToString();
                    break;
                }

                return $"{cpuInfo}-{hddInfo}";
            }
            catch
            {
                return "UNABLE_TO_RETRIEVE_HWID";
            }
        }

        public static string GenerateLicenseKey(string hardwareId)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hardwareId + LicenseSecret.Secret));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

