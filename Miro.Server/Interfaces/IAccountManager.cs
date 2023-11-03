using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> RegisterAsync(RegisterModel registerModel);
        Task<ResultModel<User>> LoginAsync(LoginModel loginModel);
    }
}
