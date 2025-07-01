using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] SignupDTO model)
        {
            var res = await _authService.Register(model);
            return Ok(res);
        }

        [HttpPost("register-with-dbcontext")]
        public async Task<IActionResult> Register([FromBody] SignupDTO model)
        {
            var res = await _authService.Register(model);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO model)
        {
            var res = await _authService.Login(model);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("secure-get")]
        public IActionResult Secure()
        {
            return Ok("You are authenticated");
        }
    }
}
