﻿using Lab2_DAW_Sgr16.Entities;

namespace TravelTalkApi.Entities
{
    public class UserWithNotification
    {
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public User User { get; set; }
        public Notification Notification { get; set; }
    }
}