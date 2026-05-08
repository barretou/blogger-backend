using Blogger.Domain.Requests.Author;
using Blogger.Services.DTO;

namespace Blogger.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto?> GetByIdAsync(Guid id);
        Task<List<AuthorDto>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<AuthorDto> CreateAsync(CreateAuthorRequest request);
    }
}
