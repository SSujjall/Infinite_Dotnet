using WebApiProj1.Models;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetById(string userId);
        User CreateUser(User model);
        GenericRes<User> UpdateUser(User model);
        GenericRes<int> DeleteUser(string userId);
    }
}
