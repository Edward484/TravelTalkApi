using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository

    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Notification>> GetByUser(User user)
        {
            ValueComparer<User> comparer = new ValueComparer<User>((a, b) => a.Id == b.Id,
                usr => usr.GetHashCode());
            return await _context.Notifications.Where(notification =>
                    notification.Receivers.Contains(user, comparer)).Include(notification => notification.Post)
                .Include(notification => notification.Topic)
                .ToListAsync();
        }

        public async Task<Notification> GetById(int id)
        {
            return await _context.Notifications.Where(n => n.NotificationId == id).FirstAsync();
        }
    }
}