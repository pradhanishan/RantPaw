using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RantPaw.Models.DTOS.UserDTOS;
using RantPaw.Models.Entities;
using RantPaw.Models.ServiceModels;
using RantPaw.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace RantPaw.Services.Server.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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

        private static  void ComputePasswordSaltAndPasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using HMACSHA512 HMAC = new();

            passwordSalt = HMAC.Key;
            passwordHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public async Task<ServiceResponse<string>> Login(LoginUserDTO loginUser)
        {
            ServiceResponse<string> response = new();

            try
            {

                User? user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Username.ToLower().Equals(loginUser.Username.ToLower()), includeProperties: null);
                if (user == null)
                {
                    // the user with given login credentials does not exist
                    response.StatusCode = StatusCodes.Status406NotAcceptable;
                    response.Message = $"user {loginUser.Username} does not exist.";
                    response.IsSuccessful = false;
                    return response;
                }

                if (!ValidatePassword(loginUser.Password, user.PasswordSalt, user.PasswordHash))
                {
                    // the user exists but the password is wrong
                    response.StatusCode = StatusCodes.Status406NotAcceptable;
                    response.Message = $"Invalid credentials";
                    response.IsSuccessful = false;
                    return response;
                }

                //Generate access token

                response.Data = GenerateAccessToken(user);
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "User logged in successfully";
                response.IsSuccessful = true;
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

        // Method to generate JWT
        private string? GenerateAccessToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role,"User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static bool ValidatePassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using HMACSHA512 HMAC = new(passwordSalt);
            var computeHash = HMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);

        }
    }
}
