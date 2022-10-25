using Newtonsoft.Json;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace RantPaw.Services.Web.PostServices
{
    public sealed class PostService : IPostService
    {

        private readonly HttpClient _httpClient;


        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<CreatePostDTO>> CreatePost(CreatePostDTO newPost)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/posts/create", newPost);
            string ResponseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<CreatePostDTO> responseData = JsonConvert.DeserializeObject<ServiceResponse<CreatePostDTO>>(ResponseAsString)!;
            return responseData;
        }

        public async Task<ServiceResponse<List<GetPostDTO>>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/posts");
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<List<GetPostDTO>> responseData = JsonConvert.DeserializeObject<ServiceResponse<List<GetPostDTO>>>(responseAsString)!;
            return responseData;

        }

        public async Task<ServiceResponse<List<GetPostDTO>>> GetAllBetween(int startingRow, int numberOfRows)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/posts/between/{startingRow}/{numberOfRows}");
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<List<GetPostDTO>> responseData = JsonConvert.DeserializeObject<ServiceResponse<List<GetPostDTO>>>(responseAsString)!;
            return responseData;
        }

        public async Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllWithReactions()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/posts/withreactions");
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<List<GetPostWithPostReactionDTO>> responseData = JsonConvert.DeserializeObject<ServiceResponse<List<GetPostWithPostReactionDTO>>>(responseAsString)!;
            return responseData;
        }

        public async Task<ServiceResponse<List<GetPostWithPostReactionDTO>>> GetAllWithReactionsBetween(int startingRow, int numberOfRows)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/posts/withreactions/between/{startingRow}/{numberOfRows}");
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<List<GetPostWithPostReactionDTO>> responseData = JsonConvert.DeserializeObject<ServiceResponse<List<GetPostWithPostReactionDTO>>>(responseAsString)!;
            return responseData;
        }

        public async Task<ServiceResponse<int>> GetPostsCount()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/posts/count");
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<int> responseData = JsonConvert.DeserializeObject<ServiceResponse<int>>(responseAsString)!;
            return responseData;
        }
    }
}
