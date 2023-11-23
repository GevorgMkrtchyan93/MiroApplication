using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IHashingPassword
    {
        string HashPassword(string password, out byte[] salt);

        bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
