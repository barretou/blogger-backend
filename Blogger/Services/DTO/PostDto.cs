using Blogger.Domain.Enums;
using Blogger.Domain.Models;

namespace Blogger.Services.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public CategoryDto Category { get; set; }
        public AuthorDto Author { get; set; }
    }
}
