using Blogger.Domain.Enums;

namespace Blogger.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
