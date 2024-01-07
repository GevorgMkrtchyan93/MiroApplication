using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Shared.AuthenticationModels;

using System;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class LoginViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private IUserDataService _userDataService;
        private readonly IHashingPassword _hashingPassword;
        
        private string _password;
        private string _email;

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

        public ICommand LoginCommand { get; set; }

        public ICommand CommandToNavigateToRegisterPage { get; set; }

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService,IUserDataService userDataService, IHashingPassword hashingPassword)
        {
            _userDataService = userDataService;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _hashingPassword = hashingPassword;
            LoginCommand = new CommandService(CanExecute_Login, Execute_Login);
            CommandToNavigateToRegisterPage = new CommandService(CanExecute_NavigateToRegisterPage, Execute_NavigateToRegisterPage);

            Email = "haruthunanyan10@gmail.com";
            Password = "Harut0777218858*";
            _hashingPassword = hashingPassword;
        }
        public bool CanExecute_NavigateToRegisterPage(object parameter)
        {
            return true;
        }

        public async void Execute_NavigateToRegisterPage(object parameter)
        {
            if (CanExecute_NavigateToRegisterPage(parameter))
              _navigationService.NavigateTo(typeof(RegisterView));
        }

        public bool CanExecute_Login(object parameter)
        {
            return true;
        }

        public async void Execute_Login(object parameter)
        {
            if (CanExecute_Login(parameter))
            {
                try
                {
                    var loginModel = new LoginModel()
                    {
                        Email = Email,
                        Password = _hashingPassword.HashPassword(Password,out var salt),
                    };
                    _userDataService.ResultInfo = await _authenticationService.Login(loginModel);
                    _userDataService.UserToken = _userDataService.ResultInfo.Data.SessionToken;
                    if (_userDataService.ResultInfo!=null)
                    {
                        _navigationService.NavigateTo(typeof(AccountView));
                    }
                    else
                        MessageBox.Show("Invalid Email or Password");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
