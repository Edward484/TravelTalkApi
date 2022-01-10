using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelTalkApi.Constants.Exceptions;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.Auth;
using TravelTalkApi.Services;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthController(
            UserManager<User> userManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO body)
        {
            var user = await _userManager.FindByEmailAsync(body.Email);

            if (user != null)
            {
                return BadRequest("The user already exists!");
            }

            if (body.Username == "")
            {
                return BadRequest("Username can't be empty");
            }

            var result = await _authService.RegisterUserAsync(body);

            if (result.Count == 0)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO body)
        {
            try
            {
                var authInfo = await _authService.LoginUserAsync(body);
                if (authInfo == null)
                {
                    return Unauthorized();
                }

                Response.Headers.AccessControlAllowOrigin = "*";

                return Ok(authInfo);
            }
            catch (Exception e)
            {
                if (e.Message.Equals(AuthExceptionStrings.WrongPassword))
                {
                    return Unauthorized(e.Message);
                }
                else if (e.Message.Equals(AuthExceptionStrings.UserNotFound))
                {
                    return NotFound(e.Message);
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshDTO body)
        {
            var newAccessToken = await _authService.RefreshAccessTokenAsync(body);
            return new OkObjectResult(new LoginResponseDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = body.RefreshToken
            });
        }
    }
}