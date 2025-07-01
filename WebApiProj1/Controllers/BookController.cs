using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBook(AddBooksDTO model)
        {
            var res = await _bookService.AddBook(model);
            return Ok(res);
        }
    }
}
