using Blogger.Domain.Models;
using Blogger.Domain.Requests.Author;

namespace Blogger.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(CreateAuthorRequest request);
    }
}
