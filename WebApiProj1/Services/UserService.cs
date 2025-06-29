using System.Reflection;
using WebApiProj1.Models;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Common;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;    
        }

        public User CreateUser(User model)
        {
            if (model is null)
            {
                return null;
            }

            _uow.UserRepository.Add(model);
            _uow.SaveChanges(); // yo chai aile etekei dekhako matra kasari use hunxa vanera in services.
            return model;
        }

        public GenericRes<int> DeleteUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var existingUser = _uow.UserRepository.GetById(userId);
                if (existingUser is null)
                {
                    return new GenericRes<int>
                    {
                        Data = 0,
                        Message = "Unable to delete, user not found!"
                    };
                }

                _uow.UserRepository.Delete(userId);
                _uow.SaveChanges(); // same like in CreateUser method
                return new GenericRes<int>
                {
                    Data = 1,
                    Message = $"User with UserId {userId} deleted succesfully"
                };
            }
            return new GenericRes<int>
            {
                Data = 0,
                Message = "User id missing"
            };
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _uow.UserRepository.GetAll();
            if (users is not null)
            {
                return users;
            }
            return null;
        }

        public User GetById(string userId)
        {
            var user = _uow.UserRepository.GetById(userId);
            if (user is not null)
            {
                return user;
            }
            return null;
        }

        public GenericRes<User> UpdateUser(User model)
        {
            if(model is not null)
            {
                var existingUser = _uow.UserRepository.GetById(model.UserId);
                if (existingUser is null)
                {
                    return new GenericRes<User>
                    {
                        Data = null,
                        Message = "user not found"
                    };
                }

                _uow.UserRepository.Update(model);
                _uow.SaveChanges(); // same like in CreateUser method
                return new GenericRes<User>
                {
                    Data = model,
                    Message = "user updated"
                };
            }

            return new GenericRes<User>
            {
                Data = null,
                Message = "model is null"
            };
        }
    }
}
