using Blogger.Domain.Enums;
using Blogger.Services.DTO;

namespace Blogger.Domain.Requests.Posts
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public int AuthorId { get; set; }
    }
}
