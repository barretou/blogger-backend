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

        public async Task<Post> CreateAsync(CreatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            Post newPost = new()
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return newPost;

        }

        public async Task<Post> UpdateAsync(UpdatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Post existingPost = await _context.Posts.FindAsync(request.Id);
            if (existingPost == null) return null;

            existingPost.Title = request.Title;
            existingPost.Content = request.Content;
            existingPost.AuthorId = request.AuthorId;
            existingPost.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingPost;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Post post = await _context.Posts.FindAsync(id);
            if (post == null) return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
