using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Services.UserService
{
    public interface IUserService
    {
        public Task<User> GetCurrentUser();
    }
}