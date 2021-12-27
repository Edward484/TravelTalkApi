using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelTalkApi.Models.DTO.Auth;

namespace TravelTalkApi.Services
{
    public interface IAuthService
    {
        Task<List<IdentityError>> RegisterUserAsync(RegisterDTO registerData);
        Task<LoginResponseDTO> LoginUserAsync(LoginDTO dto);
        Task<string> RefreshAccessTokenAsync(RefreshDTO refresh);
    }
}
