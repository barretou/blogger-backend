using Blogger.Domain.Enums;

namespace Blogger.Domain.Requests.Category
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }
    }
}
