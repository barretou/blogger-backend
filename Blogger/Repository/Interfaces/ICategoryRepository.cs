using Blogger.Domain.Enums;
using Blogger.Domain.Models;

namespace Blogger.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> GetByTypeAsync(CategoryType type);
    }
}
