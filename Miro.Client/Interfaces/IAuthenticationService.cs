using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResultModel<User>> Login(LoginModel loginModel);

        Task<ResultModel<User>> Register(RegisterModel registerModel);

        Task<bool> Logout(int userId);
    }
}
