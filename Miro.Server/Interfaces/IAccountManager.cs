using Miro.Server.Entities;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> RegisterAsync(RegisterModel registerModel);
        Task<bool> LoginAsync(LoginModel loginModel);
    }
}
