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
        private readonly ITokenService<User> _tokenService;
        private readonly GenericMapper<RegisterModel, User> _mapperRegister;
        private readonly GenericMapper<LoginModel, User> _mapperLogin;
 
        public AccountManager(IRepository<User> userRepository, ITokenService<User> tokenService)
        {
            _userRepository = userRepository;
            _mapperRegister = new GenericMapper<RegisterModel, User>();
            _mapperLogin = new GenericMapper<LoginModel, User>();
            _tokenService = tokenService;
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

                var storedUser = await _userRepository.GetAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password).ConfigureAwait(false);

                if (storedUser != null)
                {
                    resultModel = new ResultModel<User>(storedUser)
                    {
                        IsSuccess = true,
                        Message = "User succsesfuly done login",
                        Data = storedUser
                    };
                    
                    resultModel.Data.SessionToken = _tokenService.GenerateToken(user);
                    await _userRepository.UpdateAsync(resultModel.Data).ConfigureAwait(false);
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

        public async Task<ResultModel<User>> RegisterAsync(RegisterModel registerModel)
        {
            try
            {

                var user = _mapperRegister.Map(registerModel);
                Validate
                    .For(user)
                    .NotNull();

                Validate
                    .For(user.UserName)
                    .NotNull()
                    .NotEmpty();

                var existingUser = await _userRepository.GetAsync(u => u.Email == registerModel.Email && u.UserName == registerModel.UserName).ConfigureAwait(false);

                if (existingUser == null)
                {
                    user.SessionToken = _tokenService.GenerateToken(user);
                    await _userRepository.AddAsync(user).ConfigureAwait(false);
                }

                return new ResultModel<User>(user)
                {
                    IsSuccess = true,
                    Data = user,
                    Message = "User succsesfuly done registration"
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId).ConfigureAwait(false);
                if (user!=null)
                {
                    user.SessionToken = null;
                    await _userRepository.UpdateAsync(user);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
