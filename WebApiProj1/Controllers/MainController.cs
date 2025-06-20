using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private static List<Person> _globalList = new List<Person>();


        [HttpGet("get-persons")]
        public IActionResult GetAllPersons()
        {
            return Ok(_globalList);
        }

        [HttpPost("create-person")]
        public IActionResult CreatePerson([FromBody] PersonDTO model)
        {
            var personModel = new Person
            {
                Name = model.Name,
                Age = model.Age,
                Dob = model.Dob,
            };

            _globalList.Add(personModel);
            return Ok(new GenericRes<List<Person>>
            {
                Data = _globalList,
                Message = "Users fetched"
            });
        }
    }
}
