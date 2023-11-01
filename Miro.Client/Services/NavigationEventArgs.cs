using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class NavigationEventArgs<PageType> : EventArgs
    {
        public PageType View { get; }

        public NavigationEventArgs(PageType view)
        {
            View = view;
        }
    }
}
