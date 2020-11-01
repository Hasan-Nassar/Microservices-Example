using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Service.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Service.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
    [HttpPost("Login")]
        
        public async Task<IActionResult> login([FromBody] AuthenticateUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}