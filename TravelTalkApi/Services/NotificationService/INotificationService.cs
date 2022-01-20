using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Services.NotificationService
{
    public interface INotificationService
    {
        public void SendCommentNotification(int topicId);
        public void SendModTakedownNotification(int postId, string modMessage);
        public void SendUpvoteNotification(int postId);

        public Task<List<Notification>> GetAllUserNotification(User user);

    }
}