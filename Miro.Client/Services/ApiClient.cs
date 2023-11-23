using Miro.Client.Interfaces;
using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Miro.Client.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string baseApiUrl = "https://localhost:7108/";

        public ApiClient(string? baseAddress)
        {
            _httpClient = new HttpClient();
        }
        public async Task<ResultModel<User>> LoginAsync(LoginModel loginModel)
        {
            try
            {
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseApiUrl}login")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                   var userInfo =  JsonConvert.DeserializeObject<ResultModel<User>>(result);
                   userInfo.IsSuccess = true;
                   return userInfo;
                }
                else
                {
                    return new ResultModel<User>(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return new ResultModel<User>(null);
            }
        }

        public async Task<ResultModel<User>> RegisterAsync(RegisterModel loginModel)
        {
            try
            {
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseApiUrl}register")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var userInfo = JsonConvert.DeserializeObject<ResultModel<User>>(result);

                if (response.IsSuccessStatusCode)
                {
                   userInfo.IsSuccess = true;
                   return userInfo;
                }

                else
                {
                    return new ResultModel<User>(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return new ResultModel<User>(null);
            }
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            try
            {
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(userId);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseApiUrl}logout")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var userInfo = JsonConvert.DeserializeObject<bool>(result);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
    }
}
