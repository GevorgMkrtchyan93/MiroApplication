using Microsoft.AspNetCore.Mvc;

using Miro.Server.Interfaces;
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
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            return Ok(await _accountManager.RegisterAsync(registerModel).ConfigureAwait(false));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            return Ok(await _accountManager.LoginAsync(loginModel).ConfigureAwait(false));
        }
    }
}
