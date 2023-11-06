using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;

        private string  _userName;

        private string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public ICommand Logout { get; set; }
        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Logout = new CommandService(CanExecute_Logout, Execute_Logout);
        }

        private bool CanExecute_Logout(object parameter)
        {
            return true;
        }

        private async void Execute_Logout(object parameter)
        {
            _navigationService.NavigateTo(typeof(LoginView));
        }

        public void Loaded(Frame mainFrame)
        {
            _navigationService.SetFrame(mainFrame);
            _navigationService.NavigateTo<RegisterView>();
        }
    }
}
