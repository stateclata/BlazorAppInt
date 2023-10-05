using BlazorApp.Shared.Utilities;
using BlazorApp.Server.Services.UserService;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using BlazorApp.Shared.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<bool>>> RegisterAsync(UserRegister user)
        {
            var response = await _userService.Register(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginAsync(UserLogin userLogin)
        {
            var response = await _userService.Login(userLogin);
            return Ok(response);
        }
    }
}