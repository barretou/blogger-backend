namespace Blogger.Domain.Requests.Author
{
    public class CreateAuthorRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
