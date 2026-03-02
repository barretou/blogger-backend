using Blogger.Domain.Enums;
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
        private readonly ICategoryRepository _categoryRepository;

        public PostService(IPostRepository postRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();

            if (posts == null || !posts.Any())
                return new List<PostDto>();

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
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
                Id = post.Id,
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
            if (author is null)
                throw new KeyNotFoundException($"Autor {request.AuthorId} não encontrado.");

            if (!Enum.IsDefined(typeof(CategoryType), request.Type))
                throw new ArgumentException("Invalid category type.");

            Category category = await _categoryRepository.GetByTypeAsync(request.Type);
            if (category is null)
                throw new InvalidOperationException("Category not found.");

            Post post = new()
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId,
                CreatedAt = DateTime.UtcNow,
                CategoryId = category.Id
            };

            var createdPost = await _postRepository.CreateAsync(post);

            return new PostDto
            {
                Id = createdPost.Id,
                Title = createdPost.Title,
                Content = createdPost.Content,
                CreatedAt = createdPost.CreatedAt,
                Type = request.Type,
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

            Post post = await _postRepository.GetByIdAsync(request.Id);
            if (post is null)
                throw new KeyNotFoundException($"Post {request.Id} not found.");

            Author author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId);
            if (author is null)
                throw new KeyNotFoundException($"Author {request.AuthorId} not found.");

            post.Title = request.Title;
            post.Content = request.Content;
            post.AuthorId = request.AuthorId;
            post.UpdatedAt = DateTime.UtcNow;

            Post updatedPost = await _postRepository.UpdateAsync(post);

            return new PostDto
            {
                Id = updatedPost.Id,
                Title = updatedPost.Title,
                Content = updatedPost.Content,
                CreatedAt = updatedPost.CreatedAt,
                UpdatedAt = updatedPost.UpdatedAt,
                Author = new AuthorDto
                {
                    Name = author.Name,
                    Email = author.Email
                }
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post is null)
                throw new KeyNotFoundException($"Post {id} not found.");

            return await _postRepository.DeleteAsync(post);
        }
    }
}
