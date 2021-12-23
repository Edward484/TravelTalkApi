using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelTalkApi.Models.DTO.Auth;

namespace TravelTalkApi.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterDTO dto);
        Task<string> LoginUser(LoginDTO dto);
    }
}
