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
                Id = createdAuthor.Id,
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
                Id = author.Id,
                Name = author.Name,
                Email = author.Email
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Author author = _authorRepository.GetAuthorByIdAsync(id).Result;
            if (author == null)
                throw new KeyNotFoundException($"Autor {author.Id} não encontrado.");

            bool isDeleted = await  _authorRepository.DeleteAuthorAsync(author);
            return isDeleted;
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            List<Author> authors = _authorRepository.GetAllAuthorsAsync().Result;

            if (authors == null || !authors.Any())
                throw new KeyNotFoundException("Nenhum autor encontrado.");

            return await Task.FromResult(authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email
            }).ToList());
        }
    }
}
