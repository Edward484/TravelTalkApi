using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

                //TODO: Return current user
                return Ok(authInfo);
            }
            catch (Exception e)
            {
                if (e.Message.Equals(AuthExceptionStrings.WrongPassword))
                {
                    return BadRequest(e.Message);
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

        //TODO: Add logout
        //TODO: Add current
        //TODO: Add refresh
    }
}