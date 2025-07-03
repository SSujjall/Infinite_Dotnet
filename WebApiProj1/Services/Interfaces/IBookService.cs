using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Services.Interfaces
{
    public interface IBookService
    {
        Task<GenericRes<Books>> AddBook(AddBooksDTO model);
        Task<GenericRes<List<Books>>> GetAllBooks();
        Task<GenericRes<Books>> GetBookById(int id);
        Task<GenericRes<Books>> UpdateBook(UpdateBookDTO model);
        Task<GenericRes<string>> DeleteBook(int id);
    }
}
