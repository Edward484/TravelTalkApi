using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var token = await _authService.LoginUserAsync(body);

            if (token == null)
            {
                return Unauthorized();
            }

            //TODO: Return current user
            return Ok(new { token });
        }
        
        //TODO: Add logout
        //TODO: Add current
        //TODO: Add refresh
    }
}
