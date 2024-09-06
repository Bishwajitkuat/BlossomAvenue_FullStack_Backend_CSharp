

using BlossomAvenue.Service.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BlossomAvenue.Infrastructure.Cryptography
{
    public class PasswordHasher : IPasswordHasher
    {
        public void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(16);
            // hashing password
            hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: originalPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 3000,
                    numBytesRequested: 32
                )
            );
        }

        public bool VerifyPassword(string inputPassword, string storedPassword, byte[] salt)
        {
            var inputHashPassword = Convert.ToBase64String(
                                                            KeyDerivation.Pbkdf2(
                                                            password: inputPassword,
                                                            salt: salt,
                                                            prf: KeyDerivationPrf.HMACSHA256,
                                                            iterationCount: 3000,
                                                            numBytesRequested: 32
                                                                                )
                                                            );
            return inputHashPassword == storedPassword;

        }
    }
}
