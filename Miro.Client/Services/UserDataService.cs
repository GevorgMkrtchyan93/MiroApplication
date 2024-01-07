using Miro.Client.Interfaces;
using Miro.Server.Entities;
using Miro.Server.Services;

using System;

namespace Miro.Client.Services
{
    public class UserDataService : IUserDataService
    {
        public ResultModel<User>? ResultInfo { get; set; }
        public string? UserToken { get; set; }
    }
}
