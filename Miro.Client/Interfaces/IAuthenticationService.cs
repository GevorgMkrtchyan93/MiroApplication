using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Register(RegisterModel registerModel);
    }
}
