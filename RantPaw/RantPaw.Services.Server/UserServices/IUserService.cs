using RantPaw.Models.DTOS.UserDTOS;
using RantPaw.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Services.Server.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> Register(RegisterUserDTO registerUser);

        Task<ServiceResponse<string>> Login(LoginUserDTO loginUser);


    }
}
