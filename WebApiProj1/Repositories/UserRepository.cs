using WebApiProj1.ActingDB;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DumDb _dbContext;

        public UserRepository(DumDb dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User model)
        {
            _dbContext.Users.Add(model);
        }

        public void Delete(string userId)
        {
            User user = GetById(userId);

            if (user is not null)
            {
                _dbContext.Users.Remove(user);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public User GetById(string userId)
        {
            User user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (user is null)
            {
                return null;
            }
            return user;
        }

        public void Update(User model)
        {
            var existingUser = GetById(model.UserId);
            if (existingUser is not null)
            {
                if (!string.IsNullOrEmpty(existingUser.FirstName))
                    existingUser.FirstName = model.FirstName;
                if (!string.IsNullOrEmpty(existingUser.LastName))
                    existingUser.LastName = model.LastName;
                if (!string.IsNullOrEmpty(existingUser.Address))
                    existingUser.Address = model.Address;
            }
        }
    }
}
