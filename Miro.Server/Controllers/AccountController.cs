using Microsoft.AspNetCore.Mvc;
using Miro.Shared.AuthenticationModels;

namespace Miro.Server.Controllers
{
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            return Ok();
        }
    }
}
