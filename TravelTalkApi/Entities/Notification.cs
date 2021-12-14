using System.Collections.Generic;

namespace TravelTalkApi.Entities
{
    public enum NotificationType
    {
        COMMENT = 0,
        DELETE = 1,
        UPVOTE = 2
    }

    public class Notification
    {
        public int NotificationId { get; set; }
        public virtual ICollection<User> Recievers { get; set; }
        public NotificationType Type { get; set; }
        /**
         * The post this notification is related to
         */
        public int PostId { get; set; }
        /**
         * A preview of the post if this is a Comment notif
         * A message from the mod that deleted the post if it's a delete
         */
        public string Extra { get; set; }
    }
}