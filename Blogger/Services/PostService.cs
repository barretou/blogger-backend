using Blogger.Domain.Enums;
using Blogger.Domain.Models;
using Blogger.Domain.Requests.Posts;
using Blogger.Repository.Interfaces;
using Blogger.Services.DTO;
using AutoMapper;
using Blogger.Services.Interfaces;

namespace Blogger.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();

            if (posts == null || !posts.Any())
                return new List<PostDto>();

            return posts.Select(post => _mapper.Map<PostDto>(post)).ToList();
        }

        public async Task<PostDto> GetPostByIdAsync(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
                throw new KeyNotFoundException($"Post com ID {postId} não encontrado.");

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreatePostAsync(CreatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Author author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId)
                ?? throw new KeyNotFoundException($"Autor {request.AuthorId} não encontrado.");

            if (!Enum.IsDefined(typeof(CategoryType), request.Type))
                throw new ArgumentException("Invalid category type.");

            Category category = await _categoryRepository.GetByTypeAsync(request.Type)
                ?? throw new InvalidOperationException("Category not found.");

            Post post = new()
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = author.Id,
                CategoryId = category.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _postRepository.CreateAsync(post);

            Post createdPost = await _postRepository.GetByIdAsync(post.Id)
                ?? throw new InvalidOperationException("Failed to load created post.");

            return _mapper.Map<PostDto>(createdPost);
        }

        public async Task<PostDto> UpdatePostAsync(Guid postId, UpdatePostRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Category);

            Post post = await _postRepository.GetByIdAsync(postId) ?? throw new KeyNotFoundException($"Post {postId} not found.");

            if (post.AuthorId != request.AuthorId)
                throw new InvalidOperationException($"Only the same author can edit the post {post.Title}.");

            Category category = await _categoryRepository.GetByTypeAsync(request.Category.Type);
            if (category is null)
                throw new InvalidOperationException("Category not found.");


            post.Title = request.Title;
            post.Content = request.Content;
            post.AuthorId = request.AuthorId;
            post.Category = category;
            post.UpdatedAt = DateTime.UtcNow;

            Post updatedPost = await _postRepository.UpdateAsync(post);

            return _mapper.Map<PostDto>(updatedPost);
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post is null)
                throw new KeyNotFoundException($"Post {postId} not found.");

            return await _postRepository.DeleteAsync(post);
        }
    }
}
