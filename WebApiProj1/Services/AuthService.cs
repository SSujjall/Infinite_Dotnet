using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        public AuthService(IOptions<JwtConfig> jwtConfig, IAuthRepository authRepository, 
            UserManager<IdtyUser> userManager)
        {
            _jwtConfig = jwtConfig.Value;
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<GenericRes<object>> Register(SignupDTO model)
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
                return GenericRes<object>.Success(response);
            }

            return new GenericRes<object>
            {
                Data = response,
                Message = "Failed to register user"
            };
        }

        public async Task<GenericRes<object>> Login(LoginDTO model)
        {
            var user = await _authRepository.ValidateUser(model.Username, model.Password);

            if (user is null)
            {
                return new GenericRes<object>
                {
                    Data = null,
                    Message = "Invalid User or Wrong Credentials"
                };
            }

            var tokenAsync = GenerateJwtToken(user);
            return new GenericRes<object>
            {
                Data = tokenAsync.Result,
                Message = "User validated"
            };
        }

        #region JWT TOKEN GENERATOR
        private async Task<string> GenerateJwtToken(IdtyUser user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
            var issuer = _jwtConfig.ValidIssuer;
            var audience = _jwtConfig.ValidAudience;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
