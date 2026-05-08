namespace Blogger.Domain.Models
{
    public class Post
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
