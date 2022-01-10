using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelTalkApi.Constants;
using TravelTalkApi.Constants.Exceptions;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Models.DTO.Auth;
using TravelTalkApi.Repositories;
using TravelTalkApi.Utilities;

namespace TravelTalkApi.Services
{
    public class AuthService : IAuthService
    
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTUtils _jwtUtils;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JWTUtils jwtUtils,
            IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _jwtUtils = jwtUtils;
        }

        public async Task<List<IdentityError>> RegisterUserAsync(RegisterDTO registerData)
        {
            var registerUser = new User();

            registerUser.Email = registerData.Email;
            registerUser.UserName = registerData.Username;

            var result = await _userManager.CreateAsync(registerUser, registerData.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, RoleType.User);

                return new List<IdentityError>();
            }

            return result.Errors.ToList();
        }

        public async Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginData)
        {
            User user = await _userManager.FindByEmailAsync(loginData.Email);
            
            if (user != null)
            {
                var isPasswordRight = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);
                if (!isPasswordRight.Succeeded)
                {
                    throw new Exception(AuthExceptionStrings.WrongPassword);
                }
                var userWithRoles = await _repository.User.GetByIdWithRoles(user.Id);
                
                //Generate a refresh token and save it into the db
                var refreshToken = _jwtUtils.CreateRefreshToken();

                //TODO: Don't regenerate refresh token if one already exists
                userWithRoles.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(userWithRoles);
                return new LoginResponseDTO()
                {
                    AccessToken = _jwtUtils.GenerateJwtToken(userWithRoles),
                    //TODO: Send current refresh token
                    RefreshToken = _jwtUtils.CreateRefreshToken()
                };
            }

            throw new Exception(AuthExceptionStrings.UserNotFound);
        }

        public async Task<string> RefreshAccessTokenAsync(RefreshDTO refresh)
        {
            var principal = _jwtUtils.GetPrincipalFromExpiredToken(refresh.ExpiredAccessToken);
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                throw new Exception("Failed refresh. User not found");
            }

            var user = await _userManager.FindByEmailAsync(email);
            var userWithRoles = await _repository.User.GetByIdWithRoles(user.Id);

            if (user.RefreshToken != refresh.RefreshToken)
                throw new Exception("Failed refresh. Mismatch refresh token");

            var newJwtToken = _jwtUtils.GenerateJwtToken(userWithRoles);

            return newJwtToken;
        }
    }
}