using Miro.Client.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Helpers
{
    public static class HashingPassword
    {
        public static string HashPasword(string password, out byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(AuthenticationConst.keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
            salt,
                AuthenticationConst.interations,
                hashAlgorithm,
                AuthenticationConst.keySize);
            return Convert.ToHexString(hash);
        }
    }
}
