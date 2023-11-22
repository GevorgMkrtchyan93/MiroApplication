using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Server.Entities;

using System;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class AccountViewModel:NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private IUserDataService _userDataService;

        private int _userId;

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private string? _userName;
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string? _email;

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public AccountViewModel(INavigationService navigationService,IAuthenticationService authenticationService, IUserDataService userDataService)
        {
            _userDataService = userDataService;
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            Logout = new CommandService(CanExecute_Logout, Execute_Logout);
            _userDataService = userDataService;
        }

        public ICommand Logout { get; set; }

        private bool CanExecute_Logout(object parameter)
        {
            return true;
        }

        private async void Execute_Logout(object parameter)
        {
            try 
            {
                var result = await _authenticationService.Logout(_userDataService.ResultInfo.Data.Id).ConfigureAwait(false);
                if (result) 
                    _navigationService.NavigateTo(typeof(LoginView));
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
            _navigationService.NavigateTo(typeof(RegisterView));
        }

    }
}
