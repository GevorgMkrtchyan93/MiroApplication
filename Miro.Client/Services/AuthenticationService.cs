using Miro.Client.Helpers;
using Miro.Client.Interfaces;
using Miro.Shared.AuthenticationModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseApiUrl = "https://localhost:7108/";

        public AuthenticationService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            try
            {
                byte[] salt;
                string hashPassword = HashingPassword.HashPasword(loginModel.Password, out salt);
                loginModel.Password = hashPassword;

                string jsonContent = JsonSerializer.Serialize(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseApiUrl}login", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                byte[] salt;
                string hashPassword = HashingPassword.HashPasword(registerModel.Password,out salt);
                registerModel.Password = hashPassword;
                string jsonContent = JsonSerializer.Serialize(registerModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseApiUrl}register", content).ConfigureAwait(false);
                var r = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
