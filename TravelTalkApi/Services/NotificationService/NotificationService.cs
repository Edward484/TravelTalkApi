using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private IRepositoryWrapper _repositoryWrapper;


        public NotificationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async void SendCommentNotification(int topicId)
        {
            // The participants of this topic
            var participants = await _repositoryWrapper.Topic.GetParticipantsAsync(topicId);
            var notification = new Notification()
            {
                Receivers = participants,
                TopicId = topicId,
                Type = NotificationType.COMMENT
            };
            _repositoryWrapper.Notification.Create(notification);
            await _repositoryWrapper.SaveAsync();
        }

        public async void SendUpvoteNotification(int postId)
        {
            // The participants of this topic
            var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId, true);
            var notification = new Notification()
            {
                Receivers = new List<User> {post.Author},
                TopicId = post.TopicId,
                Type = NotificationType.UPVOTE,
                PostId = postId
            };
            _repositoryWrapper.Notification.Create(notification);
            await _repositoryWrapper.SaveAsync();
        }
        
        public async void SendWarningNotificationToAuthor(int postId)
        {
            // To the author of the post because of his bad behavior
            var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId, true);
            var notification = new Notification()
            {
                Receivers = new List<User> {post.Author},
                TopicId = post.TopicId,
                Type = NotificationType.WARNING,
                PostId = postId
            };
            _repositoryWrapper.Notification.Create(notification);
            await _repositoryWrapper.SaveAsync();
        }

        public async void ChangeNotificationType(int notId)
        {
            //importance of the notification increases
            var notification = await _repositoryWrapper.Notification.GetById(notId);
            notification.Type = NotificationType.ALERT;
            await _repositoryWrapper.SaveAsync();
        }

        public async void DeleteNotification(int notId)
        {
            //deleting the notification because of mistake
            var notification = await _repositoryWrapper.Notification.GetById(notId);
            _repositoryWrapper.Notification.Delete(notification);
        }

        public async Task<List<Notification>> GetAllUserNotification(User user)
        {
            var listNotifications = await _repositoryWrapper.Notification.GetByUser(user);
            return listNotifications;
        }
    }
}