using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Web.ResponseServices
{
    public interface IReactionService
    {
        Task<ServiceResponse<string>> CreatePostReaction(CreatePostReactionDTO newReaction);
    }
}
