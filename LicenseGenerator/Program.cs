using System;
using System.Security.Cryptography;
using System.Text;

namespace LicenseGenerator
{
    internal class Program
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

            string licenseKey = GenerateLicenseKey(hardwareId);
            Console.WriteLine($"Generated License Key: {licenseKey}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static string GenerateLicenseKey(string hardwareId)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hardwareId + BAS_Tools.Licensing.LicenseSecret.Secret));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

