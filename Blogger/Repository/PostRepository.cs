using Blogger.Domain.Enums;
using Blogger.Domain.Models;
using Blogger.Domain.Requests.Posts;
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
                                 .Include(a => a.Author)
                                 .Include(c => c.Category)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                                 .Include(a => a.Author)
                                 .Include(c => c.Category)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreateAsync(Post request)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _context.Posts.AddAsync(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<Post> UpdateAsync(Post request)
        {
            ArgumentNullException.ThrowIfNull(request);

            _context.Posts.Update(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<bool> DeleteAsync(Post request)
        {
            ArgumentNullException.ThrowIfNull(request);

            _context.Posts.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
