using Miro.Server.Entities;
using Miro.Server.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IUserDataService
    {
        ResultModel<User>? ResultInfo { get; set; }
    }
}
