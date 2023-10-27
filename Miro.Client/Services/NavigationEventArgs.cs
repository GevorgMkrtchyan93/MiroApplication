using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class NavigationEventArgs<TView> : EventArgs
    {
        public TView View { get; }

        public NavigationEventArgs(TView view)
        {
            View = view;
        }
    }
}
