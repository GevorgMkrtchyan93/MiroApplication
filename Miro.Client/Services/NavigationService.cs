using Miro.Client.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class NavigationService<TView> : INavigationService
    {
        private TView? currentView;

        public TView? CurrentView => currentView;

        public event EventHandler<NavigationEventArgs<TView>>? Navigated;

        public void NavigateBack()
        {

        }

        public void NavigateTo(TView? view)
        {
            if (!view.Equals(currentView))
            {
                currentView = view;
                Navigated?.Invoke(this, new NavigationEventArgs<TView>(view));
            }
        }
    }
}
