using Blogger.Domain.Enums;
using Blogger.Domain.Requests.Category;

namespace Blogger.Domain.Requests.Posts
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public CategoryType Type { get; set; }
    }
}
