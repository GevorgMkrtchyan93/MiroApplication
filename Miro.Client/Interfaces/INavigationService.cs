using Miro.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface INavigationService
    {
        public interface INavigation<TView>
        {
            void NavigateTo(TView view);

            void NavigateBack();

            TView CurrentView { get; }

            /// <summary>
            /// Event that is raised when a navigation occurs.
            /// </summary>
            event EventHandler<NavigationEventArgs<TView>> Navigated;
        }

    }
}
