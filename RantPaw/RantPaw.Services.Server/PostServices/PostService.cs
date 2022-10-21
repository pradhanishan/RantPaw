using Microsoft.AspNetCore.Http;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.Entities;
using RantPaw.Models.ServiceModels;
using RantPaw.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Server.PostServices
{
    public sealed class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all posts ordered by created date descending
        public async Task<ServiceResponse<List<GetPostDTO>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDTO>> response = new();

            List<GetPostDTO> listOfPostsForResponse = new();

            try
            {
                IEnumerable<Post> posts = await _unitOfWork.Post.GetAllAsync(includeProperties:"User");

                foreach (Post post in posts)
                {
                    listOfPostsForResponse.Add(new GetPostDTO
                    {
                        AuthorName = post.User.Username,
                        Id = post.Id,
                        Description = post.Description,
                        CreatedDate = post.CreatedDate,
                        IsAnonymous = post.IsAnonymous,
                        UpdateDate = post.UpdateDate
                    });
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.IsSuccessful = true;
                response.Data = listOfPostsForResponse;
                response.Message = "All posts fetched successfully";
                return response;


            }
            catch (Exception)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.IsSuccessful = false;
                response.Message = "An internal server error occured";
                return response;
            }



        }
    }
}
