using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly IRepository<User> _userRepository;

        public AccountManager(IRepository<User> userRepository)
        {
          _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            await _userRepository.AddAsync(loginModel).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {

            await _registerRepository.AddAsync(registerModel).ConfigureAwait(false);
            return true;
        }
    }
}
