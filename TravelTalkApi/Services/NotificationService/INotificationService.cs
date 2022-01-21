using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Services.NotificationService
{
    public interface INotificationService
    {
        public void SendCommentNotification(int topicId);
        public void SendUpvoteNotification(int postId);
        public void SendWarningNotificationToAuthor(int postId);
        void ChangeNotificationType(int notId);
        void DeleteNotification(int notId);



        public Task<List<Notification>> GetAllUserNotification(User user);

    }
}