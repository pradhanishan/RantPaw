using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using RantPaw.Services.Server.PostServices;
using System.Security.Claims;

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

        public async Task<ActionResult<ServiceResponse<List<GetPostDTO>>>> GetAllPosts()
        {
            var response = await _postService.GetAllPosts();

            return response.StatusCode switch
            {
                StatusCodes.Status200OK => (ActionResult<ServiceResponse<List<GetPostDTO>>>)Ok(response),
                _ => (ActionResult<ServiceResponse<List<GetPostDTO>>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }


        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<CreatePostDTO>>> CreatePost(CreatePostDTO newPost)
        {
            newPost.AuthorID = Int32.Parse(User.Claims!.FirstOrDefault()!.Value);

            var response = await _postService.CreatePost(newPost);

            return response.StatusCode switch
            {
                StatusCodes.Status201Created => (ActionResult<ServiceResponse<CreatePostDTO>>)CreatedAtAction("CreatePost", response),
                _ => (ActionResult<ServiceResponse<CreatePostDTO>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }

    }
}
