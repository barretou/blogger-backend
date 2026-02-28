using Blogger.Domain.Models;
using Blogger.Repository.Interfaces;
using Blogger.Services.DTO;
using Blogger.Services.Interfaces;

namespace Blogger.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();

            if (posts == null || !posts.Any())
                return new List<PostDto>();

            return posts.Select(MapToDto).ToList();
        }

        public async Task<PostDto?> GetPostByIdAsync(int id)
        {
            Post post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                throw new KeyNotFoundException($"Post com ID {id} não encontrado.");

            return MapToDto(post);
        }

        private PostDto MapToDto(Post post)
        {
            return new PostDto
            {
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Author = post.Author != null ? new AuthorDto
                {
                    Name = post.Author.Name,
                    Email = post.Author.Email
                } : null
            };
        }
    }
}
