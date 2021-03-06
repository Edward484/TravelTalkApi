using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Services.UserService
{
    public interface IUserService
    {
        public Task<User> GetCurrentUser();
        public Task<User> GetCurrentUserJoinedData();
        public Task<string> GetCurrentUserId();
    }
}