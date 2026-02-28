using Blogger.Domain.Models;
using Blogger.Repository.Context;
using Blogger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts
                                 .Include(p => p.Author)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                                 .Include(p => p.Author)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
