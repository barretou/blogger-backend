using Blogger.Domain.Requests.Posts;
using Blogger.Services.DTO;

namespace Blogger.Services.Interfaces
{
    public interface IPostService
    {
            Task<List<PostDto>> GetAllPostsAsync();
            Task<PostDto?> GetPostByIdAsync(Guid postId);
            Task<PostDto> CreatePostAsync(CreatePostRequest request);
            Task<PostDto> UpdatePostAsync(Guid postId, UpdatePostRequest request);
            Task<bool> DeletePostAsync(Guid postId);
    }
}
