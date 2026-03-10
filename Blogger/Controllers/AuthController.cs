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
            var token = await _authService.Authenticate(request.Email, request.Password);

            if (token == null)
                return Unauthorized("Credenciais inválidas");

            return Ok(new { token });
        }
    }
}
