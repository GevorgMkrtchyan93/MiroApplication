using Miro.Client.Interfaces;
using Miro.Shared.AuthenticationModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<bool> LoginAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}
