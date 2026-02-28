using Blogger.Domain.Requests.Posts;
using Blogger.Services.DTO;

namespace Blogger.Services.Interfaces
{
    public interface IPostService
    {
            Task<List<PostDto>> GetAllAsync();
            Task<PostDto?> GetByIdAsync(int id);
            Task<PostDto> CreateAsync(CreatePostRequest request);
            Task<PostDto> UpdateAsync(UpdatePostRequest request);
            Task<bool> DeleteAsync(int id);
    }
}
