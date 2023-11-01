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
                // Convert the loginModel to JSON
                string jsonContent = JsonSerializer.Serialize(loginModel);

                // Create a StringContent with JSON data
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send a POST request to your backend's login endpoint
                var response = await _httpClient.PostAsync($"{baseApiUrl}login", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Successful login
                    return true;
                }

                // Handle login failure
                return false;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return false;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                // Convert the registerModel to JSON
                string jsonContent = JsonSerializer.Serialize(registerModel);

                // Create a StringContent with JSON data
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send a POST request to your backend's registration endpoint
                var response = await _httpClient.PostAsync($"{baseApiUrl}register", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Successful registration
                    return true;
                }

                // Handle registration failure
                return false;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return false;
            }
        }
    }
}
