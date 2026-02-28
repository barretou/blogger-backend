using Blogger.Services.DTO;

namespace Blogger.Services.Interfaces
{
    public interface IPostService
    {
            Task<List<PostDto>> GetAllPostsAsync();
            Task<PostDto?> GetPostByIdAsync(int id);
    }
}
