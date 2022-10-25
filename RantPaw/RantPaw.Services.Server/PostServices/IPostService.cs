using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Server.PostServices
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDTO>>> GetAllPosts();

        Task<ServiceResponse<CreatePostDTO>> CreatePost(CreatePostDTO newPost);

        Task<ServiceResponse<List<GetPostDTO>>> GetAllPostsBetween(int startingRow, int numberOfRows);

        Task<ServiceResponse<int>> GetAllPostsCount();

        Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllPostsWithReactions();

        Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllPostsWithReactionsBetween(int startingRow, int numberOfRows);


    }
}
