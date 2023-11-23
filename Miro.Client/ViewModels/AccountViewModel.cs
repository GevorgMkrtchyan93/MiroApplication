using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;
using Miro.Server.Entities;

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class AccountViewModel : NotifyPropertyChange
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
                _userId = _userDataService.ResultInfo.Data.Id;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private string? _userName;
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = _userDataService.ResultInfo.Data.UserName;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string? _email;

        public string? Email
        {
            get => _userDataService.ResultInfo.Data.Email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public AccountViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserDataService userDataService)
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
            if (_userDataService.ResultInfo.Data.Id != null)
                return true;
            return false;
        }

        private async void Execute_Logout(object parameter)
        {
            if (CanExecute_Logout(parameter))
            {
                try
                {
                    var result = await _authenticationService.Logout(_userDataService.ResultInfo.Data.Id).ConfigureAwait(false);

                    // Ensure UI updates are done on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (result)
                            _navigationService.NavigateTo(typeof(LoginView));
                        else
                            _navigationService.NavigateTo(typeof(RegisterView));
                    });
                }
                catch (Exception ex)
                {
                    // Ensure UI updates are done on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message.ToString());
                    });
                }
            }
        }

    }
}
