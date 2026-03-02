using Blogger.Domain.Enums;

namespace Blogger.Services.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public CategoryType Type { get; set; }
        public AuthorDto Author { get; set; }
    }
}
