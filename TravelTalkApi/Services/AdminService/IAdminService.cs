using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;

namespace TravelTalkApi.Services.AdminService
{
    public interface IAdminService
    {
        public void GiveUserRole(string username, string role);
        public void RemoveUserRole(string username, string role);
        public void MakeUserCategoryMod(string username, int categoryId);
        public void RemoveUserCategoryMod(string username, int categoryId);
    }
}