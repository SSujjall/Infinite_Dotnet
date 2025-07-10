using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Roles> _roleManager;
        public RoleController(RoleManager<Roles> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("get-all-roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var role = new Roles
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
            };

            var res = await _roleManager.CreateAsync(role);
            if (res.Succeeded)
            {
                return Ok(role);
            }
            return BadRequest();
        }
    }
}
