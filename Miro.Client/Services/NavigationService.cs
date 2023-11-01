using Miro.Client.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Miro.Client.Services
{
    public class NavigationService : INavigationService
    {
        private Type currentView;
        private readonly IServiceProvider serviceProvider;
        private Frame? frame;

        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Type CurrentView => currentView;

        public event EventHandler<NavigationEventArgs<Type>> Navigated;
        public void SetFrame(Frame frame)
        {
            this.frame = frame;
        }

        public void GoBack()
        {
            frame.GoBack();
        }

        public void NavigateTo(Type pageType, object? parameter = null)
        {
            var view = serviceProvider.GetService(pageType);
            frame.Navigate(view);
        }

        public void NavigateTo<TView>(object? parameter = null)
          where TView : Page
        {
            NavigateTo(typeof(TView), parameter);
        }

        public async Task NavigateTo<TParameter>(Type pageType, TParameter param, object? parameter = null)
        {
            var view = serviceProvider.GetService(pageType) as FrameworkElement;
            frame.Navigate(view, parameter);

            if (view?.DataContext is INavigatable<TParameter> navigatableViewModel)
                await navigatableViewModel.OnNavigate(param).ConfigureAwait(false);
        }

        public Task NavigateTo<TView, TParameter>(TParameter param, object? parameter = null) where TView : Page
        {
            return NavigateTo(typeof(TView), param, parameter);
        }
    }
}
