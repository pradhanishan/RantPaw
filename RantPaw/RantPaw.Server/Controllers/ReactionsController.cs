using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using RantPaw.Services.Server.ReactionServices;

namespace RantPaw.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionsController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> React(CreatePostReactionDTO newPostReaction)
        {


            var response = await _reactionService.CreatePostReaction(newPostReaction);

            return response.StatusCode switch
            {
                StatusCodes.Status400BadRequest => (ActionResult<ServiceResponse<string>>)BadRequest(response),
                StatusCodes.Status201Created => (ActionResult<ServiceResponse<string>>)CreatedAtAction("React", response),
                _ => (ActionResult<ServiceResponse<string>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }


    }
}
