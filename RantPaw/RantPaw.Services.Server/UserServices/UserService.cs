using Microsoft.AspNetCore.Http;
using RantPaw.Models.DTOS;
using RantPaw.Models.Entities;
using RantPaw.Models.ServiceModels;
using RantPaw.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace RantPaw.Services.Server.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<string>> Register(RegisterUserDTO registerUser)
        {
            ServiceResponse<string> response = new();

            try
            {
                // Check if username is taken
                if (await _unitOfWork.User.CheckIfExists(u => u.Username.ToLower().Equals(registerUser.Username.ToLower())))
                {
                    response.StatusCode = StatusCodes.Status406NotAcceptable;
                    response.IsSuccessful = false;
                    response.Message = $"username {registerUser.Username} is taken";
                    response.Data = registerUser.Username;
                    return response;
                }

                // Generate password hash and password salt

                ComputePasswordSaltAndPasswordHash(registerUser.Password, out byte[] passwordSalt, out byte[] passwordHash);

                User user = new()
                {
                    Username = registerUser.Username.ToLower(),
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                };

                await _unitOfWork.User.CreateAsync(user);
                await _unitOfWork.SaveAsync();

                response.StatusCode = StatusCodes.Status201Created;
                response.IsSuccessful = true;
                response.Message = $"User {registerUser.Username} registered successfully";
                response.Data = registerUser.Username.ToLower();
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

        // method to generate password salt and password hash from new register user's input password

        private static void ComputePasswordSaltAndPasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using HMACSHA512 HMAC = new();

            passwordSalt = HMAC.Key;
            passwordHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

    }
}
