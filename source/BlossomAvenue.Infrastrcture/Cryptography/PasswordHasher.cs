

using BlossomAvenue.Service.Cryptography;
using System.Security.Cryptography;

namespace BlossomAvenue.Infrastrcture.Cryptography
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // PBKDF2 hashing logic
            using var rng = new Rfc2898DeriveBytes(password, 16, 10000, HashAlgorithmName.SHA256);
            var salt = rng.Salt;
            var hash = rng.GetBytes(32);

            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var bytes = Convert.FromBase64String(hashedPassword);
            var salt = bytes[..16];
            var hash = bytes[16..];

            using var rng = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            var computedHash = rng.GetBytes(32);

            return hash.SequenceEqual(computedHash);
        }
    }
}
