using Microsoft.IdentityModel.Tokens;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Models;
using NASAProj.Service.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NASAProj.Service.Extensions;
using NASAProj.Service.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace NASAProj.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> repository;
        private readonly IConfiguration configuration;

        public AuthService(IGenericRepository<User> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<string> GenerateToken(string username, string password)
        {
            User user = await repository.GetAsync(u =>
                u.Email == username && u.Password.Equals(password.Encrypt()));

            if (user is null)
                throw new HttpStatusCodeException(400, "Login or Password is incorrect");

            var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(int.Parse(configuration["JWT:Expire"])),
                claims: new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                },
                signingCredentials: new SigningCredentials(
                    key: authSigningKey,
                    algorithm: SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
