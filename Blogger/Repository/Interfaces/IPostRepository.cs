using Blogger.Domain.Models;
using Blogger.Domain.Requests.Posts;

namespace Blogger.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(int id);
        Task<List<Post>> GetAllAsync();
        Task<Post> CreateAsync(CreatePostRequest request);
        Task<Post> UpdateAsync(UpdatePostRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
