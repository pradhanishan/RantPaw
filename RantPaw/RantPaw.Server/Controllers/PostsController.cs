using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Services.Server.PostServices;

namespace RantPaw.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Get the list of all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public async Task<ActionResult<List<GetPostDTO>>> GetAllPosts()
        {
            var response = await _postService.GetAllPosts();

            return response.StatusCode switch
            {
                StatusCodes.Status200OK => (ActionResult<List<GetPostDTO>>)Ok(response),
                _ => (ActionResult<List<GetPostDTO>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }


    }
}
