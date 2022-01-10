using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.User;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUserJoinedData();
                return new OkObjectResult(new CurrentUserDTO(currentUser));
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
    }
}