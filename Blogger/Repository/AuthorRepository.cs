using Blogger.Domain.Models;
using Blogger.Domain.Requests.Author;
using Blogger.Repository.Context;
using Blogger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAuthorAsync(CreateAuthorRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            Author newAuthor = new()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            };
            await _context.Authors.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
            return newAuthor;
        }
    }
}
