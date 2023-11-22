using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Interfaces
{
    public interface IAccountManager
    {
        Task<ResultModel<User>> RegisterAsync(RegisterModel registerModel);

        Task<ResultModel<User>> LoginAsync(LoginModel loginModel);

        Task<bool> LogoutAsync(int userId);
    }
}
