using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Models.DTO.Admin;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.AdminService;

namespace TravelTalkApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AdminController(IAdminService adminService,IRepositoryWrapper repositoryWrapper)
        {
            _adminService = adminService;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("current")]
        [Authorize("User")]
        public async Task<ActionResult<ICollection<UserRole>>> GetRole(int adminId)
        {
            try
            {
                var adminWithRoles = await _repositoryWrapper.User.GetByIdWithRoles(adminId);
                return new OkObjectResult(adminWithRoles.UserRoles);
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost("admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> MakeAdmin(AdminDTO body)
        {
            _adminService.GiveUserRole(body.Username, RoleType.Admin);
            return new NoContentResult();
        }

        [HttpDelete("admin")]
        [Authorize("admin")]
        public async Task<IActionResult> RemoveAdmin(AdminDTO body)
        {
            _adminService.RemoveUserRole(body.Username, RoleType.Admin);
            return new NoContentResult();
        }

        [HttpPost("mod")]
        [Authorize("Admin")]
        public async Task<IActionResult> MakeMod(AdminDTO body)
        {
            _adminService.GiveUserRole(body.Username, RoleType.Mod);
            return new NoContentResult();
        }

        [HttpDelete("mod")]
        [Authorize("Admin")]
        public async Task<IActionResult> RemoveMod(AdminDTO body)
        {
            _adminService.RemoveUserRole(body.Username, RoleType.Mod);
            return new NoContentResult();
        }

        [HttpPost("mod/{categoryId:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> MakeCategoryMod(AdminDTO body, int categoryId)
        {
            _adminService.MakeUserCategoryMod(body.Username, categoryId);
            return new NoContentResult();
        }

        [HttpDelete("mod/{categoryId:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> RemoveAdmin(AdminDTO body, int categoryId)
        {
            _adminService.RemoveUserCategoryMod(body.Username, categoryId);
            return new NoContentResult();
        }

        [HttpPatch("promoteMod")]
        [Authorize("Admin")]
        public async Task<ActionResult> PromoteToAdmin(AdminDTO body)
        {
            _adminService.RemoveUserRole(body.Username, RoleType.Mod);
            _adminService.GiveUserRole(body.Username, RoleType.Admin);

            return new NoContentResult();
        }
    }
}