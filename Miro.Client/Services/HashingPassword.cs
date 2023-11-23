using Miro.Client.Consts;
using Miro.Client.Interfaces;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Miro.Client.Services
{
    public class HashingPassword : IHashingPassword
    {
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private const int _keySize = AuthenticationConst.keySize;
        private const int _iterations = AuthenticationConst.interations;

        public string HashPassword(string password, out byte[] salt)
        {

            salt = new byte[32];
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                hashAlgorithm,
                _keySize);

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, hashAlgorithm, _keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
