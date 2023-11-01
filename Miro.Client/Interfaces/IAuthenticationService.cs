using Miro.Shared.AuthenticationModels;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(LoginModel loginModel);
        Task<bool> RegisterAsync(RegisterModel registerModel);
    }
}
