using Miro.Server.Entities;
using Miro.Server.Services;

namespace Miro.Client.Interfaces
{
    public interface IUserDataService
    {
        ResultModel<User>? ResultInfo { get; set; }

        string? UserToken { get; set; }
    }
}
