using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Server.Services;
using System.Collections;

namespace Miro.Server.Controllers
{
    public class UserController : Controller
    {
        private readonly ITokenService<User> _tokenService;
        private readonly IRepository<User> _userRepository;

        public UserController(ITokenService<User> tokenService, IRepository<User> userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("userByToken")]
        public async Task<ActionResult<ResultModel<User>>> GetUserByToken([FromBody]string token)
        {
            return Ok(await _tokenService.GetByTokenAsync(token).ConfigureAwait(false));
        }

        [HttpPost("userById")]

        public async Task<ActionResult<ResultModel<User>>> GetUserById([FromBody] int id)
        {
            return Ok(await _userRepository.GetByIdAsync(id).ConfigureAwait(false));
        }

        [HttpPost("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return Ok(await _userRepository.GetAllAsync().ConfigureAwait(false));
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateUserByToken([FromBody]User user, string token)
        {
            return Ok(await _tokenService.UpdateTokenAsync(user,token).ConfigureAwait(false));
        }
    }
}
