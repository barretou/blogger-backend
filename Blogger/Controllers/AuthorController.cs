using Blogger.Domain.Requests.Author;
using Blogger.Domain.Requests.Posts;
using Blogger.Services.DTO;
using Blogger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            try
            {
                var post = await _authorService.GetByIdAsync(id);
                return Ok(post);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _authorService.GetAllAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreateAuthorRequest request)
        {
            try
            {
                var createdAuthor = await _authorService.CreateAsync(request);
                return StatusCode(201, createdAuthor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool isDeleted = await _authorService.DeleteAsync(id);
                if (isDeleted)
                    return NoContent();
                else
                    return StatusCode(500, new { message = "Erro ao deletar o autor." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
     }
}
