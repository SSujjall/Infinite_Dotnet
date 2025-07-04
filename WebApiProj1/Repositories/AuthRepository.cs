using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApiProj1.Data;
using WebApiProj1.Models;
using WebApiProj1.Models.Config;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Interfaces;

namespace WebApiProj1.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdtyUser> _userManager;
        private readonly SignInManager<IdtyUser> _signInManager;

        private readonly AppDbContext _dbContext;


        public AuthRepository(UserManager<IdtyUser> userManager, SignInManager<IdtyUser> signInManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }


        public async Task<IdtyUser?> ValidateUser(string username, string password)
        {
            //var user = await _dbContext.Users.FindAsync(username);
            //if (user is null)
            //{
            //    return null;
            //}

            //var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            //return result.Succeeded ? user : null;

            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? user : null;
        }

        public async Task<IdentityResult> CreateNewUser(IdtyUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityUser> CreateNewUserUsingContext(IdentityUser user, string password)
        {
            var result = await _dbContext.AddAsync(user);
            return result.Entity;
        }
    }
}
