using Blogger.Domain.Requests.Posts;
using Blogger.Services.DTO;

namespace Blogger.Services.Interfaces
{
    public interface IPostService
    {
            Task<List<PostDto>> GetAllAsync();
            Task<PostDto?> GetByIdAsync(Guid id);
            Task<PostDto> CreateAsync(CreatePostRequest request);
            Task<PostDto> UpdateAsync(Guid id, UpdatePostRequest request);
            Task<bool> DeleteAsync(Guid id);
    }
}
