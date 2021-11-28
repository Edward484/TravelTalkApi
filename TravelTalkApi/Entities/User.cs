using System.Collections;
using System.Collections.Generic;
using Lab2_DAW_Sgr16.Entities;

namespace TravelTalkApi.Entities
{
    public class User
    {
        public int UserId { get; set; }
        
        public int RoleId { get; set; }
        public Role Role { get; set; }
        
        public string Username { get; set; }
        public string Hash { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
    }
}