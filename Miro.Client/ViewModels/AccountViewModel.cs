using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class AccountViewModel:NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;


        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public AccountViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Logout = new CommandService(CanExecute_Logout,Execute_Logout);
        }

        public ICommand Logout { get; set; }

        private bool CanExecute_Logout(object parameter)
        {
            return true;
        }

        private async  void Execute_Logout(object parameter)
        {
            try 
            { 

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
            _navigationService.NavigateTo(typeof(RegisterView));
        }

    }
}
