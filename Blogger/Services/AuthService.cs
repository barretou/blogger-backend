using Blogger.Domain.Models;
using Blogger.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogger.Services
{
    public class AuthService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IConfiguration _config;

        public AuthService(IAuthorRepository authorRepository, IConfiguration config)
        {
            _authorRepository = authorRepository;
            _config = config;
        }

        public async Task<string?> Authenticate(string email, string password)
        {
            var author = await _authorRepository.GetByEmailAsync(email);

            if (author == null)
                return null;

            if (author.Password != password)
                return null;

            return GenerateToken(author);
        }

        private string GenerateToken(Author author)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, author.Id.ToString()),
            new Claim(ClaimTypes.Name, author.Name),
            new Claim(ClaimTypes.Email, author.Email)
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
