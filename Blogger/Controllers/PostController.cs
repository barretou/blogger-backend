using Blogger.Services.DTO;
using Blogger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<PostDto>>> GetAllPosts()
        {
            var result = await _postService.GetAllPostsAsync();
            return Ok(result); // retorna lista vazia se não houver posts
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            return Ok(post);
        }
    }
}
