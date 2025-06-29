using WebApiProj1.Models.Entities;

namespace WebApiProj1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(string userId);
        IEnumerable<User> GetAll();
        void Add(User model);
        void Delete(string userId);
        void Update(User model);
    }
}
