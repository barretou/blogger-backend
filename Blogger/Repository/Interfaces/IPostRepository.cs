using Blogger.Domain.Models;

namespace Blogger.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(int id);
        Task<List<Post>> GetAllAsync();
    }
}
