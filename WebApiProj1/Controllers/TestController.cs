using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        [HttpGet("get")]
        public int Get()
        {
            return 1;
        }

        //public string Sum(int a, int b)
        //{
        //    return (a + b).ToString();
        //}

        [HttpGet("my-func")]
        public string MyFunction()
        {
            try
            {
                return Convert.ToString(Sum(1, 2));
            }
            catch (Exception ex)
            {
                return "Exception";
            }
            finally
            {
                Console.WriteLine("finaly");
            }
        }

        private int Sum(int a, int b)
        {
            return a + b;
        }

        [HttpGet("get-even-number")]
        public List<int> ADADAD()
        {
            var list = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };

            var anotherList = new List<int>();

            foreach(var item in list)
            {
                if (item % 2 == 0)
                {
                    anotherList.Add(item);
                }
            }

            return anotherList;
        }

        [HttpGet("get-person")]
        public IEnumerable<Person> GetPerson()
        {
            var person = new Person
            {
                Name = "Test",
                Age = 123,
                Dob = new DateTime(2003, 12, 25)
            };

            return new List<Person> { person };
        }

        //[HttpGet]
        //public Person GetP()
        //{
        //    var model = new Person(Guid.NewGuid().ToString(), "sujal", 1, new DateTime(2003, 12, 25));
        //    return model;
        //}
    }
}
