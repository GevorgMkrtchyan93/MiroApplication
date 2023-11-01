﻿using Miro.Client.Interfaces;
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
                string jsonContent = JsonSerializer.Serialize(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseApiUrl}login", content);

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
                string jsonContent = JsonSerializer.Serialize(registerModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseApiUrl}register", content);

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
