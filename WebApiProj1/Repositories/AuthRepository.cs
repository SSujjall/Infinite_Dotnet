using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApiProj1.Models;
using WebApiProj1.Models.Config;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Interfaces;

namespace WebApiProj1.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityUser?> ValidateUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? user : null;
        }

        public async Task<IdentityResult> CreateNewUser(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
