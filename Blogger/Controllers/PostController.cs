using Blogger.Services.DTO;
using Blogger.Domain.Requests.Posts;
using Microsoft.AspNetCore.Mvc;
using Blogger.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Blogger.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> Get()
        {
            var result = await _postService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                return Ok(post);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostRequest request)
        {
            try
            {
                var createdPost = await _postService.CreateAsync(request);
                return StatusCode(201, createdPost);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Update(int id, [FromBody] UpdatePostRequest request)
        {
            if (id != request.Id)
                return BadRequest(new { message = "Id da rota diferente do corpo da requisição." });

            try
            {
                var updatedPost = await _postService.UpdateAsync(request);
                return Ok(updatedPost);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _postService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
