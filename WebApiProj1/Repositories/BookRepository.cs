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
            _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }
    }
}
