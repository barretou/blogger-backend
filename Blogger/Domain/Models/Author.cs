namespace Blogger.Domain.Models
{
    public class Author
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
