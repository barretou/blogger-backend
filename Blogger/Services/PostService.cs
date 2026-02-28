using Blogger.Domain.Models;
using Blogger.Domain.Requests.Posts;
using Blogger.Repository.Interfaces;
using Blogger.Services.DTO;
using Blogger.Services.Interfaces;

namespace Blogger.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;

        public PostService(IPostRepository postRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();

            if (posts == null || !posts.Any())
                return new List<PostDto>();

            return posts.Select(post => new PostDto
            {
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Author = post.Author != null ? new AuthorDto
                {
                    Name = post.Author.Name,
                    Email = post.Author.Email
                } : null
            }).ToList();
        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                throw new KeyNotFoundException($"Post com ID {id} não encontrado.");

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

        public async Task<PostDto> CreateAsync(CreatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Author author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId);
            if (author == null)
                throw new KeyNotFoundException($"Autor {request.AuthorId} não encontrado.");


            Post createdPost = await _postRepository.CreateAsync(request);
            return new PostDto
            {
                Title = createdPost.Title,
                Content = createdPost.Content,
                CreatedAt = createdPost.CreatedAt,
                Author = new AuthorDto
                {
                    Name = author.Name,
                    Email = author.Email
                }
            };
        }

        public async Task<PostDto> UpdateAsync(UpdatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Post updatedPost = await _postRepository.UpdateAsync(request);

            if (updatedPost == null)
                throw new KeyNotFoundException($"Post {request.Id} não encontrado.");

            var author = await _authorRepository.GetAuthorByIdAsync(updatedPost.AuthorId);

            return new PostDto
            {
                Title = updatedPost.Title,
                Content = updatedPost.Content,
                CreatedAt = updatedPost.CreatedAt,
                Author = author != null ? new AuthorDto
                {
                    Name = author.Name,
                    Email = author.Email
                } : null
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _postRepository.DeleteAsync(id);

            if (!deleted)
                throw new KeyNotFoundException($"Post com ID {id} não encontrado.");

            return true;
        }
    }
}
