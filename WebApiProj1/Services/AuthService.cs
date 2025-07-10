using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProj1.Models;
using WebApiProj1.Models.Config;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories.Interfaces;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<IdtyUser> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IOptions<JwtConfig> jwtConfig, IAuthRepository authRepository, 
            UserManager<IdtyUser> userManager, RoleManager<Roles> roleManager,
            ILogger<AuthService> logger)
        {
            _jwtConfig = jwtConfig.Value;
            _authRepository = authRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<GenericRes<object>> Register(SignupDTO model)
        {
            try
            {
                var user = new IdtyUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FullName = model.FullName,
                };

                var response = await _authRepository.CreateNewUser(user, model.Password);
                //var res = await _authRepository.CreateNewUserUsingContext(user, model.Password);

                if (response.Succeeded)
                {
                    #region add user role
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new Roles() { Id = Guid.NewGuid().ToString(), Name = "User" });
                    }

                    await _userManager.AddToRoleAsync(user, "User");
                    #endregion

                    return GenericRes<object>.Success(null, "Register success");
                }

                return GenericRes<object>.Failed(response.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured when registering in.");
                return GenericRes<object>.Failed(null, "Exception Occured");
            }
        }

        public async Task<GenericRes<object>> Login(LoginDTO model)
        {
            try
            {
                var user = await _authRepository.ValidateUser(model.Username, model.Password);

                if (user is null)
                {
                    return GenericRes<object>.Failed(null, "Invalid User or Wrong Credentials");
                }

                var tokenAsync = await GenerateJwtToken(user);
                return GenericRes<object>.Success(tokenAsync, "Login Success");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Exception occured when loggin in.");
                return GenericRes<object>.Failed(null, "Exception Occured");
            }
        }

        #region JWT TOKEN GENERATOR
        private async Task<string> GenerateJwtToken(IdtyUser user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
            var issuer = _jwtConfig.ValidIssuer;
            var audience = _jwtConfig.ValidAudience;

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
