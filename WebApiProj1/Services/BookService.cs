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

        public async Task<GenericRes<string>> DeleteBook(int id)
        {
            var existingBook = await _repo.GetById(id);
            if (existingBook == null)
            {
                return new GenericRes<string>
                {
                    Data = null,
                    Message = "There is no book to delete"
                };
            }

            await _repo.DeleteBook(existingBook);
            return new GenericRes<string>
            {
                Data = null,
                Message = "Book deleted"
            };
        }

        public async Task<GenericRes<List<Books>>> GetAllBooks()
        {
            var res = await _repo.GetAllBooks();
            return new GenericRes<List<Books>>
            {
                Data = res,
                Message = "All Books Listed"
            };
        }

        public async Task<GenericRes<Books>> GetBookById(int id)
        {
            var res = await _repo.GetById(id);
            if (res is null)
            {
                return new GenericRes<Books>
                {
                    Data = null,
                    Message = $"There is no book with id: {id}"
                };
            }
            return new GenericRes<Books>
            {
                Data = res,
                Message = "Book loaded by id"
            };
        }

        public async Task<GenericRes<Books>> UpdateBook(UpdateBookDTO model)
        {
            var existingBook = await _repo.GetById(model.BookId);
            if (existingBook is null)
            {
                return new GenericRes<Books>
                {
                    Data = null,
                    Message = "There is no book to update"
                };
            }

            existingBook.BookName = model.BookName;
            existingBook.BookPrice = model.BookPrice;

            var res = await _repo.UpdateBook(existingBook);
            if (res is null)
            {
                return new GenericRes<Books>
                {
                    Data = null,
                    Message = "Error while updating"
                };
            }
            return new GenericRes<Books>
            {
                Data = res,
                Message = "Book updated"
            };
        }
    }
}
