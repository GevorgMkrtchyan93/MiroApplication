using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IHttpCallManager
    {
        Task<T> PostAsync<T>(string endPoint, object data);

    }
}
