using Newtonsoft.Json;
using RantPaw.Models.DTOS.UserDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Web.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> Login(LoginUserDTO loginUser)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/users/login", loginUser);
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<string> responseData = JsonConvert.DeserializeObject<ServiceResponse<string>>(responseAsString)!;
            return responseData;
        }
        
        public async Task<ServiceResponse<string>> Register(RegisterUserDTO registerUser)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/users/register", registerUser);
            string responseAsString = await response.Content.ReadAsStringAsync();
            ServiceResponse<string> responseData = JsonConvert.DeserializeObject<ServiceResponse<string>>(responseAsString)!;
            return responseData;
        }
    }
}
