using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;

using System.Windows.Controls;

namespace Miro.Client.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Loaded(Frame mainFrame)
        {
            _navigationService.SetFrame(mainFrame);
            _navigationService.NavigateTo<RegisterView>();
        }
    }
}
