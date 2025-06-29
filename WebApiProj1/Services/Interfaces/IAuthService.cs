using Microsoft.AspNetCore.Identity;
using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;

namespace WebApiProj1.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GenericRes<object>> Register(SignupDTO model);
        Task<GenericRes<object>> Login(LoginDTO model);
    }
}
