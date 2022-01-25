using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.Auth;
using TravelTalkApi.Models.DTO.User;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;




        public UserController(
            IUserService userService,
            IRepositoryWrapper repository,
            UserManager<User> userManager,
            IAuthService authService)
        {
            _userService = userService;
            _repository = repository;
            _userManager = userManager;
            _authService = authService;
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

        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult> CreateUserManual(RegisterDTO body)
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

        [HttpDelete("{userId:int}")]
        [Authorize("Admin")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await _repository.User.GetByIdComplete(userId);
                _repository.User.Delete(user);
                await _repository.SaveAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
        
        //TODO: PATCH edit username
    }
}