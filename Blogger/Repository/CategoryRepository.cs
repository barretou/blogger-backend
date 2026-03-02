using Blogger.Domain.Enums;
using Blogger.Domain.Models;
using Blogger.Repository.Context;
using Blogger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category?> GetByTypeAsync(CategoryType type)
        {
            return await _context.Categories
                                 .FirstOrDefaultAsync(c => c.Type == type); ;
        }
    }
}
