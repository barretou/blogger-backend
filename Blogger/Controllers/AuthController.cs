using Blogger.Domain.Requests.Auth;
using Blogger.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var result = await _authService.Authenticate(request.Email, request.Password);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
