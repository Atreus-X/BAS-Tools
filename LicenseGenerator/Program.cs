using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using BAS_Tools.Licensing;

namespace LicenseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Hardware ID:");
            string hardwareId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(hardwareId))
            {
                Console.WriteLine("Hardware ID cannot be empty.");
                return;
            }

            string licenseKey = GenerateLicense(hardwareId);
            Console.WriteLine($"Generated License Key: {licenseKey}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static string GenerateLicense(string hardwareId)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hardwareId + LicenseSecret.SecretKey));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

