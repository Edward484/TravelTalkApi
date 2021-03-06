using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTalkApi.Entities
{
    public enum NotificationType
    {
        COMMENT = 0,
        UPVOTE = 1,
        WARNING = 2,
        ALERT = 3
    }

    public class Notification
    {
        public int NotificationId { get; set; }
        public virtual ICollection<User> Receivers { get; set; }
        [Required]
        public NotificationType Type { get; set; }
        /**
         * The post this notification is related to
         * It's null if the notification is related only to a topic
         */
        public Nullable<int> PostId { get; set; }
        public Post Post { get; set; }
        
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
    }
}