using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRepositoryWrapper _repository;


        public UserController(
            IUserService userService,
            IRepositoryWrapper repository)
        {
            _userService = userService;
            _repository = repository;
        }

        [HttpGet("current")]
        [Authorize("User")]
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

        [HttpPatch("change")]
        [Authorize("User")]
        public async Task<IActionResult> ChangeCurrentUser(ChangeUserDTO body)
        {
            try
            {
                var currentUserId = await _userService.GetCurrentUserId();
                _repository.User.UpdateUserName(body.NewUsername, int.Parse(currentUserId));
                await _repository.SaveAsync();

            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }
    }
}