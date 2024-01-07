using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
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
        private IUserDataService _userDataService;
        private readonly IHashingPassword _hashingPassword;

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

        public RegisterViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserDataService userDataService, IHashingPassword hashingPassword)
        {
            _userDataService = userDataService;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _hashingPassword = hashingPassword;

            RegisterCommand = new CommandService(CanExecute_Register, Execute_Register);
            CommandToNavigateToLoginPage = new CommandService(CanExecute_CommandToNavigateLoginPage, Execute_CommandNavigateToLoginPage);

            Email = "haruthunanyan10@gmail.com";
            UserName = "Harutyun";
            Password = "Harut0777218858*";
            ConfirmPassword = "Harut077218858*";
            _hashingPassword = hashingPassword;
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
                        Password = _hashingPassword.HashPassword(Password,out var salt),
                        ConfirmPassword = ConfirmPassword
                    };

                    _userDataService.ResultInfo = await _authenticationService.Register(registerViewModel);
                    _userDataService.UserToken = _userDataService.ResultInfo.Data.SessionToken;

                    if (_userDataService.ResultInfo != null)
                    {
                        _navigationService.NavigateTo(typeof(AccountView));
                    }
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
