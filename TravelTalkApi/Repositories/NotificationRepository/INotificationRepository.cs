using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public interface INotificationRepository: IGenericRepository<Notification>
    {
        Task<List<Notification>> GetByUser(User user);
    }
}