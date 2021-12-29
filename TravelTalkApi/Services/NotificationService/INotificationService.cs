namespace TravelTalkApi.Services.NotificationService
{
    public interface INotificationService
    {
        public void SendCommentNotification(int topicId);
        public void SendModTakedownNotification(int postId, string modMessage);
        public void SendUpvoteNotification(int postId);
    }
}