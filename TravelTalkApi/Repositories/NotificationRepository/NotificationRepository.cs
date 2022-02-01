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
            ValueComparer<User> comparer = new ValueComparer<User>((a, b) => a != null && b != null && a.Id == b.Id,
                usr => usr.GetHashCode());
            var a = _context.Notifications.Include(notification => notification.Receivers);
            var b = a.Where(notification => notification.Receivers.Contains(user));
            var c = b.Include(notification => notification.Post);
            var d = c.Include(notification => notification.Topic)
                .ToListAsync();
            return await d;
        }

        public async Task<Notification> GetById(int id)
        {
            return await _context.Notifications.Where(n => n.NotificationId == id).FirstAsync();
        }
    }
}