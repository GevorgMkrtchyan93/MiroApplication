using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Shared.AuthenticationModels;
using Miro.Shared.Validation;

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Miro.Client.ViewModels
{
    public class RegisterViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private string _password;
        private string _email;
        private string _userName;

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

        public RegisterViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            RegisterCommand = new CommandService(CanExecute_Register, Execute_Register);
        }

        private bool CanExecute_Register(object parameter)
        {
            return true;
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
                        Password = Password
                    };

                    //_validationService.Validate(registerViewModel);

                    bool result = await _authenticationService.RegisterAsync(registerViewModel);
                    if (result)
                        _navigationService.NavigateTo(typeof(MainView));
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
