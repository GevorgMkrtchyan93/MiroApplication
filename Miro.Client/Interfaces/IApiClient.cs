using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IApiClient
    {
        Task<ResultModel<User>> RegisterAsync(RegisterModel data);

        Task<ResultModel<User>> LoginAsync(LoginModel data);

        Task<bool> LogoutAsync(int userId);
    }
}
