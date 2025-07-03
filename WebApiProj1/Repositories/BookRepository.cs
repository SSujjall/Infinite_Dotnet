using Microsoft.EntityFrameworkCore;
using WebApiProj1.Data;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Interfaces;

namespace WebApiProj1.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;
        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Books> CreateBook(Books model)
        {
            var result = await _dbContext.Books.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteBook(Books model)
        {
            _dbContext.Books.Remove(model);
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Books> GetById(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookId == id);
            return book;
        }

        public async Task<Books> UpdateBook(Books model)
        {
            var book = _dbContext.Update(model);
            return book.Entity;
        }
    }
}
