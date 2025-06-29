using Microsoft.AspNetCore.Identity;
using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;

namespace WebApiProj1.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateNewUser(IdentityUser user, string password);
        Task<IdentityUser> ValidateUser(string username, string password);
    }
}
