using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RantPaw.Models.DTOS.UserDTOS;
using RantPaw.Models.ServiceModels;
using RantPaw.Services.Server.UserServices;

namespace RantPaw.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost("Register")]

        public async Task<ActionResult<ServiceResponse<string>>> Register(RegisterUserDTO registerUser)
        {
            var response = await _userService.Register(registerUser);

            return response.StatusCode switch
            {
                StatusCodes.Status201Created => (ActionResult<ServiceResponse<string>>)CreatedAtAction("Register", response),
                StatusCodes.Status406NotAcceptable => (ActionResult<ServiceResponse<string>>)BadRequest(response),
                _ => (ActionResult<ServiceResponse<string>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }

        [HttpPost("Login")]

        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginUserDTO loginUser)
        {
            var response = await _userService.Login(loginUser);

            return response.StatusCode switch
            {
                StatusCodes.Status200OK => (ActionResult<ServiceResponse<string>>)Ok(response),
                StatusCodes.Status406NotAcceptable => (ActionResult<ServiceResponse<string>>)BadRequest(response),
                _ => (ActionResult<ServiceResponse<string>>)Problem(statusCode: StatusCodes.Status500InternalServerError, detail: response.Message, title: response.Message)
            };

        }


    }
}
