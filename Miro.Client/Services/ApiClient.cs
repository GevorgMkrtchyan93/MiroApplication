﻿using Miro.Client.Helpers;
using Miro.Client.Interfaces;
using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
                byte[] salt;
                string hashPassword = HashingPassword.HashPasword(loginModel.Password, out salt);
                loginModel.Password = hashPassword;

                string jsonContent = System.Text.Json.JsonSerializer.Serialize(loginModel);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseApiUrl}register")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ResultModel<User>>(r);
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
                byte[] salt;
                string hashPassword = HashingPassword.HashPasword(loginModel.Password, out salt);
                loginModel.Password = hashPassword;

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
    }
}
