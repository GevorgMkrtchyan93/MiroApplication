using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Server.Entities;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

using System;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class RegisterViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        private string _password;
        private string _email;
        private string _userName;
        private string _confirmPassword;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand CommandToNavigateToLoginPage { get; set; }

        public RegisterViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            RegisterCommand = new CommandService(CanExecute_Register, Execute_Register);
            CommandToNavigateToLoginPage = new CommandService(CanExecute_CommandToNavigateLoginPage, Execute_CommandNavigateToLoginPage);

            Email = "haruthunanyan10@gmail.com";
            UserName = "Harutyun";
            Password = "Harut0777218858*";
            ConfirmPassword = "Harut077218858*";
        }


        private bool CanExecute_CommandToNavigateLoginPage(object parameter)
        {
            return true;
        }
        private bool CanExecute_Register(object parameter)
        {
            return true;
        }

        private async void Execute_CommandNavigateToLoginPage(object parameter)
        {
            if (CanExecute_CommandToNavigateLoginPage(parameter))
                _navigationService.NavigateTo(typeof(LoginView));
        }

        private async void Execute_Register(object parameter)
        {
            if (CanExecute_Register(parameter))
            {
                try
                {
                    var registerViewModel = new RegisterModel()
                    {
                        Email = Email,
                        UserName = UserName,
                        Password = Password,
                        ConfirmPassword = ConfirmPassword
                    };

                    ResultModel<User> resultInfo = await _authenticationService.Register(registerViewModel);
                    if (resultInfo != null)
                        _navigationService.NavigateTo(typeof(AccountView),resultInfo);
                    else
                        MessageBox.Show("Invalid Email or Password");
                }
                catch (Exception ex) 
                {
                     MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
