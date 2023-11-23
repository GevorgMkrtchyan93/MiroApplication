using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IRelayCommand
    {
        bool CanExecute(object parameter);

        void Execute(object parameter);
    }
}
