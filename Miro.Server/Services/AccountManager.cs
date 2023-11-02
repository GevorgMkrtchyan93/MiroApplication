using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Shared.AuthenticationModels;
using Miro.Shared.Map;
using Miro.Shared.Validation;

namespace Miro.Server.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly GenericMapper<RegisterModel, User> _mapperRegister;
        private readonly GenericMapper<LoginModel, User> _mapperLogin;
        public AccountManager(IRepository<User> userRepository)
        {
           _userRepository = userRepository;
           _mapperRegister = new GenericMapper<RegisterModel, User>();
           _mapperLogin = new GenericMapper<LoginModel, User>();
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            var user = _mapperLogin.Map(loginModel);
            Validate
              .For(user)
              .NotNull();
            Validate
              .For(user.UserName)
              .NotNull();
            await _userRepository.AddAsync(user).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            var user = _mapperRegister.Map(registerModel);
            Validate
                .For(user)
                .NotNull();

            Validate
                .For(user.UserName)
                .NotNull()
                .NotEmpty();
            await _userRepository.AddAsync(user).ConfigureAwait(false);
            return true;
        }
    }
}
