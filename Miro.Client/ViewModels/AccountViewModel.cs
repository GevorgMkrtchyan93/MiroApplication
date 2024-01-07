using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;

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
        private readonly IUserManager _userManager;

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

        public AccountViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserDataService userDataService, IUserManager userManager)
        {
            _userDataService = userDataService;
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            Logout = new CommandService(CanExecute_Logout, Execute_Logout);
            JoinBoard = new CommandService(CanExecute_JoinBoard, Execute_JoinBoard);
            _userDataService = userDataService;
            _userManager = userManager;
            GetUserInfo();
        }

        private async Task GetUserInfo()
        {
            var userInfo = await _userManager.GetUserByTokenId(_userDataService.UserToken).ConfigureAwait(false);
            Email = userInfo.Email;
            UserId = userInfo.Id;
            UserName = userInfo.UserName;
        }
        public ICommand Logout { get; set; }

        public ICommand JoinBoard { get; set; }

        private bool CanExecute_Logout(object parameter)
        {
            if (_userDataService.ResultInfo.Data.Id != null)
                return true;
            return false;
        }

        private bool CanExecute_JoinBoard(object parameter)
        {
            return true; 
        }

        private async void Execute_JoinBoard(object parameter)
        {
            try
            {
                _navigationService.NavigateTo(typeof(BoardView));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
