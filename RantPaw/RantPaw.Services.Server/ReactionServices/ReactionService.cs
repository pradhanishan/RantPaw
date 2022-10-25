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

namespace RantPaw.Services.Server.ReactionServices
{
    public sealed class ReactionService : IReactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponse<string>> CreatePostReaction(CreatePostReactionDTO newPostReaction)
        {
            ServiceResponse<string> response = new();

            try
            {
                // find post to update

                Post? post = await _unitOfWork.Post.GetFirstOrDefaultAsync(p => p.Id == newPostReaction.PostId, includeProperties: null);

                if (post == null)
                {
                    response.IsSuccessful = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "The post you are trying to react to does not exist";
                    return response;
                }

                // check if user has already reacted to post

                if (await _unitOfWork.Reaction.CheckIfExists(r => r.PostId == newPostReaction.PostId && r.UserId == newPostReaction.UserId && r.ReactionId == newPostReaction.ReactionId))
                {
                    response.IsSuccessful = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "You have already made this reaction to the current post";
                    return response;
                }

                // add new reaction;

                PostReaction postReaction = new()
                {
                    PostId = newPostReaction.PostId,
                    UserId = newPostReaction.UserId,
                    ReactionId = newPostReaction.ReactionId
                };

                await _unitOfWork.Reaction.CreateAsync(postReaction);
                await _unitOfWork.SaveAsync();

                response.IsSuccessful = true;
                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Reacted successfully";
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
