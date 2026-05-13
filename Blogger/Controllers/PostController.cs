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
            var result = await _postService.GetAllPostsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(Guid id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);
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
                var createdPost = await _postService.CreatePostAsync(request);
                return StatusCode(201, createdPost);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Update(Guid id, [FromBody] UpdatePostRequest request)
        {
            try
            {
                var updatedPost = await _postService.UpdatePostAsync(id, request);
                return Ok(updatedPost);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _postService.DeletePostAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
