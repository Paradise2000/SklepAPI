using Microsoft.AspNetCore.Mvc;
using SklepAPI.Models;
using SklepAPI.Services;

namespace SklepAPI.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterDto dto)
        {
            _userService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _userService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
