using Miro.Client.Interfaces;
using Miro.Client.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Miro.Client.ViewModels
{
    public class MainWindowViewModel 
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
