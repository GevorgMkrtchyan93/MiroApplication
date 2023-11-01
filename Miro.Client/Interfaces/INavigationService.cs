using Miro.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Miro.Client.Interfaces
{
    public interface INavigationService
    {
        void SetFrame(Frame frame);

        void NavigateTo(Type pageType, object? parameter = null);

        void NavigateTo<T>(object? parameter = null) where T : Page;

        Task NavigateTo<TParameter>(Type pageType, TParameter param, object? parameter = null);

        Task NavigateTo<TView, TParameter>(TParameter param, object? parameter = null)
            where TView : Page;

        void GoBack();
    }
}
