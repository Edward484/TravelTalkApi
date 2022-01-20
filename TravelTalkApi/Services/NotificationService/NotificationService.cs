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

        public async void SendModTakedownNotification(int postId, string modMessage)
        {
            // The participants of this topic
            var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId, true);
            var notification = new Notification()
            {
                Receivers = new List<User> {post.Author},
                TopicId = post.TopicId,
                Type = NotificationType.DELETE,
                Extra = modMessage,
                PostId = postId
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

        public async Task<List<Notification>> GetAllUserNotification(User user)
        {
            var listNotifications = await _repositoryWrapper.Notification.GetByUser(user);
            return listNotifications;
        }
    }
}