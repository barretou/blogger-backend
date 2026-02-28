using Blogger.Domain.Models;
using Blogger.Domain.Requests.Author;
using Blogger.Repository.Interfaces;
using Blogger.Services.DTO;
using Blogger.Services.Interfaces;

namespace Blogger.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<AuthorDto> CreateAsync(CreateAuthorRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Author createdAuthor = await _authorRepository.CreateAuthorAsync(request);

            return new AuthorDto
            {
                Name = createdAuthor.Name,
                Email = createdAuthor.Email
            };
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            Author author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                throw new KeyNotFoundException($"Autor {author.Id} não encontrado.");

            return new AuthorDto
            {
                Name = author.Name,
                Email = author.Email
            };
        }
    }
}
