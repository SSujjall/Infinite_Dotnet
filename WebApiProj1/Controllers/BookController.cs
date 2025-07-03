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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetBooks()
        {
            var res = await _bookService.GetAllBooks();
            return Ok(res);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBooks(int id)
        {
            var res = await _bookService.GetBookById(id);
            return Ok(res);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook(UpdateBookDTO model)
        {
            var res = await _bookService.UpdateBook(model);
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _bookService.DeleteBook(id);
            return Ok(res);
        }
    }
}
