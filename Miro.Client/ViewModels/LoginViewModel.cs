using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Shared.AuthenticationModels;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Miro.Client.ViewModels
{
    public class LoginViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        
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

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            LoginCommand = new CommandService(CanExecute_Login, Execute_Login);
            CommandToNavigateToRegisterPage = new CommandService(CanExecute_NavigateToRegisterPage, Execute_NavigateToRegisterPage);
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
                        Password = Password,
                    };
                    bool result = await _authenticationService.LoginAsync(loginModel);
                    if (result)
                        _navigationService.NavigateTo(typeof(MainView));
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
