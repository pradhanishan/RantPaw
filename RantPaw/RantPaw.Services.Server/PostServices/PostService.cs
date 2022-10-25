using AutoMapper;
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
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Create new post
        public async Task<ServiceResponse<CreatePostDTO>> CreatePost(CreatePostDTO newPost)
        {
            ServiceResponse<CreatePostDTO> response = new();

            try
            {
                Post createdPost = await _unitOfWork.Post.CreateAsync(_mapper.Map<CreatePostDTO, Post>(newPost));
                await _unitOfWork.SaveAsync();

                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Post created successfully.";
                response.IsSuccessful = true;
                response.Data = newPost;
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

        // Get all posts ordered by created date descending
        public async Task<ServiceResponse<List<GetPostDTO>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDTO>> response = new();

            List<GetPostDTO> listOfPostsForResponse = new();

            try
            {
                IEnumerable<Post> posts = await _unitOfWork.Post.GetAllAsync(includeProperties: "User");

                foreach (Post post in posts)
                {
                    listOfPostsForResponse.Add(new GetPostDTO
                    {
                        AuthorName = post.IsAnonymous ? "Anonymous" : post.User.Username,
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

        public async Task<ServiceResponse<List<GetPostDTO>>> GetAllPostsBetween(int startingRow, int numberOfRows)
        {
            ServiceResponse<List<GetPostDTO>> response = new();

            List<GetPostDTO> listOfPostsForResponse = new();

            try
            {
                IEnumerable<Post> posts = await _unitOfWork.Post.GetBetweenAsync(startingRow, numberOfRows, includeProperties: "User");

                foreach (Post post in posts)
                {
                    listOfPostsForResponse.Add(new GetPostDTO
                    {
                        AuthorName = post.IsAnonymous ? "Anonymous" : post.User.Username,
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

        public async Task<ServiceResponse<int>> GetAllPostsCount()
        {
            ServiceResponse<int> response = new();

            try
            {
                response.Data = await _unitOfWork.Post.GetCountAsync(filter: null);
                response.StatusCode = StatusCodes.Status200OK;
                response.IsSuccessful = true;
                response.Message = "Data fetched successfully";
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

        public async Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllPostsWithReactions()
        {
            ServiceResponse<List<GetPostWithPostReactionDTO>> response = new();
            try
            {
                List<GetPostWithPostReactionDTO> responseData = new();

                IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(includeProperties: null);

                IEnumerable<PostWithPostReaction> postsWithPostReactions = await _unitOfWork.Post.GetPostsWithReactions();

                foreach (PostWithPostReaction postWithPostReaction in postsWithPostReactions)
                {
                    GetPostWithPostReactionDTO postWithPostReactionDTO = new()
                    {
                        AuthorName = postWithPostReaction.IsAnonymous ? "anonymous" : users.Where(u => u.Id == postWithPostReaction.AuthorId).Select(u => u.Username).FirstOrDefault()!,
                        Description = postWithPostReaction.Description,
                        CreatedDate = postWithPostReaction.CreatedDate,
                        UpdateDate = postWithPostReaction.UpdateDate,
                        AuthorId = postWithPostReaction.AuthorId,
                        IsAnonymous = postWithPostReaction.IsAnonymous
                    };

                    foreach (PostReaction postReaction in postWithPostReaction.Reactions)
                    {
                        GetPostReactionDTO postReactionDTO = new()
                        {
                            Reaction = postReaction.Reaction!.Name,
                            ReactorId = postReaction.User!.Id,
                            ReactorName = postReaction.User.Username,

                        };
                        postWithPostReactionDTO.Reactions.Add(postReactionDTO);
                    }
                    responseData.Add(postWithPostReactionDTO);
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.IsSuccessful = true;
                response.Data = responseData;
                response.Message = "Data fetched successfully";
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

        public async Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllPostsWithReactionsBetween(int startingRow, int numberOfRows)
        {
            ServiceResponse<List<GetPostWithPostReactionDTO>> response = new();
            try
            {
                List<GetPostWithPostReactionDTO> responseData = new();

                IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(includeProperties: null);

                IEnumerable<PostWithPostReaction> postsWithPostReactions = await _unitOfWork.Post.GetPostsWithReactionsBetween(startingRow, numberOfRows);

                foreach (PostWithPostReaction postWithPostReaction in postsWithPostReactions)
                {
                    GetPostWithPostReactionDTO postWithPostReactionDTO = new()
                    {
                        AuthorName = postWithPostReaction.IsAnonymous ? "anonymous" : users.Where(u => u.Id == postWithPostReaction.AuthorId).Select(u => u.Username).FirstOrDefault()!,
                        Description = postWithPostReaction.Description,
                        CreatedDate = postWithPostReaction.CreatedDate,
                        UpdateDate = postWithPostReaction.UpdateDate,
                        AuthorId = postWithPostReaction.AuthorId,
                        IsAnonymous = postWithPostReaction.IsAnonymous
                    };

                    foreach (PostReaction postReaction in postWithPostReaction.Reactions)
                    {
                        GetPostReactionDTO postReactionDTO = new()
                        {
                            Reaction = postReaction.Reaction!.Name,
                            ReactorId = postReaction.User!.Id,
                            ReactorName = postReaction.User.Username,

                        };
                        postWithPostReactionDTO.Reactions.Add(postReactionDTO);
                    }
                    responseData.Add(postWithPostReactionDTO);
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.IsSuccessful = true;
                response.Data = responseData;
                response.Message = "Data fetched successfully";
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
