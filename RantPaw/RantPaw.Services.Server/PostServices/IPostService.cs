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

        //Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO newPost);

    }
}
