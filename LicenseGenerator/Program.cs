using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LicenseGenerator
{
    class Program
    {
        private const string SECRET_SALT = "tXA9/Cxbp7lSJ66Hwd5gGMGd5QorvhG1T5jOCgAx6Z0="; // MUST MATCH THE ONE IN LicenseManager.cs

        static void Main(string[] args)
        {
            Console.WriteLine("BAS-Tools License Key Generator");
            Console.WriteLine("-------------------------------");

            while (true)
            {
                Console.Write("Enter Hardware ID (or 'exit' to close): ");
                string hardwareId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(hardwareId))
                    continue;

                if (hardwareId.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                string licenseKey = GenerateLicenseKey(hardwareId);

                Console.WriteLine($"Generated License Key: {licenseKey}");
                Console.WriteLine();
            }
        }

        private static string GenerateLicenseKey(string hardwareId)
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
    }
}
