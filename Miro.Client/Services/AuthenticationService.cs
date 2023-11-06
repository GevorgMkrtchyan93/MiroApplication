using Microsoft.AspNet.SignalR.Messaging;

using Miro.Client.Consts;
using Miro.Client.Helpers;
using Miro.Client.Interfaces;
using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;
using Miro.Shared.Validation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Miro.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApiClient _apiClient;
       

        public AuthenticationService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                Validate.For<string>(loginModel.Email).NotEmpty().Regex(ValidationConstants.EmailPattern);
                Validate.For<string>(loginModel.Password).NotNull().NotEmpty().MinValue(ValidationConstants.MinPasswordLength).MaxValue(ValidationConstants.MaxPasswordLength).Regex(ValidationConstants.PasswordPattern);

                var response = await _apiClient.LoginAsync(loginModel).ConfigureAwait(false);

                if (response.IsSuccess)
                {
                    return true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString());}
            return false;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            try
            {
                Validate.For<string>(registerModel.UserName).NotEmpty().MinLength(ValidationConstants.MinUsernameLength).MaxLength(ValidationConstants.MaxUsernameLength);
                Validate.For<string>(registerModel.Email).NotNull().NotEmpty().Regex(ValidationConstants.EmailPattern);
                Validate.For<string>(registerModel.Password).NotNull().NotEmpty().MinLength(ValidationConstants.MinPasswordLength).MaxLength(ValidationConstants.MaxPasswordLength).Regex(ValidationConstants.PasswordPattern);
                Validate.For<string>(registerModel.ConfirmPassword).NotNull().NotEmpty().MinLength(ValidationConstants.MinPasswordLength).MaxLength(ValidationConstants.MaxPasswordLength).Regex(ValidationConstants.PasswordPattern);

                var response = await _apiClient.RegisterAsync(registerModel).ConfigureAwait(false);

                if (response.IsSuccess)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }
        
    }
}
