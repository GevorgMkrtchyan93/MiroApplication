using Microsoft.AspNetCore.Mvc;

using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Server.Services;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResultModel<User>>> Register([FromBody] RegisterModel registerModel)
        {
            return Ok(await _accountManager.RegisterAsync(registerModel).ConfigureAwait(false));
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResultModel<User>>> Login([FromBody] LoginModel loginModel)
        {
            return Ok(await _accountManager.LoginAsync(loginModel).ConfigureAwait(false));
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout([FromBody]int userId)
        {
            return Ok(await _accountManager.LogoutAsync(userId).ConfigureAwait(false));
        }
    }
}
