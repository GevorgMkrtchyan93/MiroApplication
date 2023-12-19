using Miro.Client.Interfaces;
using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpCallManager _callManager;

        public ApiClient(IHttpCallManager callManager)
        {
            _callManager = callManager;
        }

        public async Task<ResultModel<User>> LoginAsync(LoginModel loginModel)
        {
            return await _callManager.PostAsync<ResultModel<User>>("login", loginModel);
        }

        public async Task<ResultModel<User>> RegisterAsync(RegisterModel registerModel)
        {
            return await _callManager.PostAsync<ResultModel<User>>("register", registerModel).ConfigureAwait(false);
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            return await _callManager.PostAsync<bool>("logout", userId);

        }
    }
}
