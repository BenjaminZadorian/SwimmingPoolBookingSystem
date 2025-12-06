using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SoftwareEngineerProject.Services
{
    public class HashingService
    {
        public static (byte[], string) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return (salt, hashed);
        }

        public static bool ValidatePassword(byte[] dbSalt, string inputPassword, string dbHash)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: dbSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            if (dbHash == hashed)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
