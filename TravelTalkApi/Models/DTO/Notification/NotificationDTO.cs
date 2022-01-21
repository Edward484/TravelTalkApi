using System.Collections.Generic;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Models.DTO.Notification
{
    public class NotificationDTO
    {
        public NotificationType Type { get; set; }
        public Entities.Post Post { get; set; }
        public Topic Topic { get; set; }

        public NotificationDTO(Entities.Notification notification)
        {
            Type = notification.Type;
            Post = notification.Post;
            Topic = notification.Topic;
        }
    }
}