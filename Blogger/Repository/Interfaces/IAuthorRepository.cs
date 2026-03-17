using Blogger.Domain.Models;
using Blogger.Domain.Requests.Author;

namespace Blogger.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetByEmailAsync(string email);
        Task<bool> DeleteAuthorAsync(Author author);
        Task<Author> CreateAuthorAsync(CreateAuthorRequest request);
    }
}
