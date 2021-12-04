using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories.GenericRepository;

namespace TravelTalkApi.Repositories.NotificationRepository
{
    public interface INotificationRepository: IGenericRepository<Notification>
    {
        Task<List<Notification>> GetByUser(User user);
    }
}