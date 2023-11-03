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

        public async Task<ResultModel<User>> LoginAsync(LoginModel loginModel)
        {
            var user = _mapperLogin.Map(loginModel);
            ResultModel<User> resultModel;
            try
            {
                Validate
                    .For(user)
                    .NotNull();

                Validate
                    .For(user.Email)
                    .NotNull();

                var results = await _userRepository.GetAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password).ConfigureAwait(false);

                if (results != null)
                {
                    resultModel = new ResultModel<User>(results);
                }
                else
                {
                    resultModel = new ResultModel<User>(null)
                    {
                        IsSuccess = false,
                        Message = "Login failed. Invalid email or password."
                    };
                }
            }
            catch (Exception ex)
            {
                resultModel = new ResultModel<User>(null)
                {
                    IsSuccess = false,
                    Message = "An error occurred during login: " + ex.Message
                };
            }

            return resultModel;
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
