using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Prog_POE.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine the salt and password hash for storage
            string saltBase64 = Convert.ToBase64String(salt);
            return $"{saltBase64}:{hashedPassword}";
        }

        public static bool VerifyPassword(string storedHash, string password)
        {
            try
            {
                // Split the stored hash into its salt and hash components
                string[] parts = storedHash.Split(':');
                if (parts.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                string storedPasswordHash = parts[1];

                // Hash the provided password with the stored salt
                string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // Compare the computed hash with the stored hash
                return storedPasswordHash == computedHash;
            }
            catch
            {
                // If any exception occurs (e.g., invalid Base64 string), verification fails
                return false;
            }
        }
    }
}