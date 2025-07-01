using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Interfaces;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo)
        {
            _repo = repo;   
        }

        public async Task<GenericRes<Books>> AddBook(AddBooksDTO model)
        {
            var reqModel = new Books
            {
                BookName = model.BookName,
                BookPrice = model.BookPrice
            };

            var res = await _repo.CreateBook(reqModel);

            if (res is not null)
            {
                return new GenericRes<Books>
                {
                    Data = res,
                    Message = "Book Added"
                };
            }

            return new GenericRes<Books>
            {
                Data = null,
                Message = "Failed to add book"
            };
        }
    }
}
