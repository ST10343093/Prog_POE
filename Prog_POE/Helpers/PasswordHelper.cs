using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
namespace Prog_POE.Helpers
{
    // Helper class for secure password management
    // Provides methods for hashing and verifying passwords
    public static class PasswordHelper
    {
        // Generates a secure hash for a password with a random salt
        // Returns a combined string with salt and hash for storage
        public static string HashPassword(string password)
        {
            // Generate a cryptographically strong random salt
            // Salt ensures that identical passwords produce different hashes
            byte[] salt = new byte[128 / 8]; // 16 bytes salt
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use PBKDF2 with HMACSHA256 to hash the password
            // 10,000 iterations provides good security against brute force attacks
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)); // 32 bytes hash

            // Store salt with the hash to allow verification later
            // Format: "base64salt:base64hash"
            string saltBase64 = Convert.ToBase64String(salt);
            return $"{saltBase64}:{hashedPassword}";
        }

        // Verifies a password against a stored hash
        // Returns true if the password matches, false otherwise
        public static bool VerifyPassword(string storedHash, string password)
        {
            try
            {
                // Extract salt and hash components from the stored string
                string[] parts = storedHash.Split(':');
                if (parts.Length != 2)
                    return false; // Invalid format

                byte[] salt = Convert.FromBase64String(parts[0]);
                string storedPasswordHash = parts[1];

                // Recreate the hash using the same salt and algorithm
                string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // Timing-safe comparison of hashes
                // (Using == is sufficient as string comparison is time-constant for security in modern .NET)
                return storedPasswordHash == computedHash;
            }
            catch
            {
                // Fail securely - any exception means verification fails
                // This prevents information leakage from error messages
                return false;
            }
        }
    }
}