using Microsoft.AspNetCore.Identity;
using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateNewUser(IdtyUser user, string password);
        Task<IdentityUser> CreateNewUserUsingContext(IdentityUser user, string password);
        Task<IdentityUser> ValidateUser(string username, string password);
    }
}
