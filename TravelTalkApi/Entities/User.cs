using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTalkApi.Entities
{
    public class User
    {
        public int UserId { get; set; }
        
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Hash { get; set; }
        public ICollection<Post> Posts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        [Required]
        public int NewNotifications { get; set; }
        
        // A list of the categories this user is mod to
        public virtual ICollection<Category> CategoryMod { get; set; }
    }
}