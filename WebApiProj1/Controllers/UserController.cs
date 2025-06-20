using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Services;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var res = _userService.GetAllUsers();
            return Ok(res);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(string id)
        {
            var res = _userService.GetById(id);
            if (res is not null)
            {
                return Ok(res);
            }
            return NotFound("User not found");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateUserDTO model)
        {
            var reqModel = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
            };

            var res = _userService.CreateUser(reqModel);
            if (res is null)
            {
                return BadRequest("Failed to create user");
            }

            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] User model)
        {
            var res = _userService.UpdateUser(model);
            if (res.Data is null)
            {
                return BadRequest("Failed to update user");
            }

            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var res = _userService.DeleteUser(id);
            if (res.Data == 1)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }
    }
}
