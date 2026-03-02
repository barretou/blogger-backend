using Blogger.Domain.Models;
using Blogger.Domain.Requests.Posts;

namespace Blogger.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(int id);
        Task<List<Post>> GetAllAsync();
        Task<Post> CreateAsync(Post request);
        Task<Post> UpdateAsync(Post request);
        Task<bool> DeleteAsync(Post request);
    }
}
