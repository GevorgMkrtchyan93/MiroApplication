using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Shared.Validation
{
    public interface IValidation<TView> where TView : class
    {
        bool IsValid { get; }
        ICollection<string> ValidationErrors { get; }

        void Validate(TView parameter);
    }
}
