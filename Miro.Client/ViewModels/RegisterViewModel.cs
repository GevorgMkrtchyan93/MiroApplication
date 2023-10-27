using Miro.Client.Interfaces;
using Miro.Client.Services;
using System;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class RegisterViewModel
    {
        private readonly INavigationService _navigationService;

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICommandService RegisterCommand { get; }

        public RegisterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            RegisterCommand = new CommandService(CanExecute, Execute);
        }

        private bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(UserName) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        private void Execute(object parameter)
        {
        }
    }
}
