namespace Blogger.Services.DTO
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public AuthorDto Author { get; set; }
    }
}
