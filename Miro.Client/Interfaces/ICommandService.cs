using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface ICommandService
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);

        event EventHandler CanExecuteChanged;
    }
}
