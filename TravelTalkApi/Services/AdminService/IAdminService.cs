using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;

namespace TravelTalkApi.Services.AdminService
{
    public interface IAdminService
    {
        public void GiveUserRole(int userId, string role);
        public void RemoveUserRole(int userId, string role);
        public void MakeUserCategoryMod(int userId, int categoryId);
        public void RemoveUserCategoryMod(int userId, int categoryId);
    }
}