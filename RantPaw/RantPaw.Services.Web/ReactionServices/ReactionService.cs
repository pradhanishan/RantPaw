using Newtonsoft.Json;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Web.ResponseServices
{
    public sealed class ReactionService : IReactionService
    {
        private readonly HttpClient _httpClient;

        public ReactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> CreatePostReaction(CreatePostReactionDTO newReaction)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/reactions/create", newReaction);
            string ResponseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<string> responseData = JsonConvert.DeserializeObject<ServiceResponse<string>>(ResponseAsString)!;
            return responseData;
        }
    }
}
