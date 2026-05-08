namespace Blogger.Services.DTO
{
    public class AuthDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
