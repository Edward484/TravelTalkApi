using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Models.DTO.Admin;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.AdminService;

namespace TravelTalkApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AdminController
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("/admin")]
        public async Task<IActionResult> MakeAdmin(AdminDTO body)
        {
            _adminService.GiveUserRole(body.UserId, RoleType.Admin);
            return new NoContentResult();
        }

        [HttpDelete("/admin")]
        public async Task<IActionResult> RemoveAdmin(AdminDTO body)
        {
            _adminService.RemoveUserRole(body.UserId, RoleType.Admin);
            return new NoContentResult();
        }

        [HttpPost("/mod")]
        public async Task<IActionResult> MakeMod(AdminDTO body)
        {
            _adminService.GiveUserRole(body.UserId, RoleType.Mod);
            return new NoContentResult();
        }

        [HttpDelete("/mod")]
        public async Task<IActionResult> RemoveMod(AdminDTO body)
        {
            _adminService.RemoveUserRole(body.UserId, RoleType.Mod);
            return new NoContentResult();
        }

        [HttpPost("/mod/{categoryId:int}")]
        public async Task<IActionResult> MakeCategoryMod(AdminDTO body, int categoryId)
        {
            _adminService.MakeUserCategoryMod(body.UserId, categoryId);
            return new NoContentResult();
        }

        [HttpDelete("/mod/{categoryId:int}")]
        public async Task<IActionResult> RemoveAdmin(AdminDTO body, int categoryId)
        {
            _adminService.RemoveUserCategoryMod(body.UserId, categoryId);
            return new NoContentResult();
        }
    }
}