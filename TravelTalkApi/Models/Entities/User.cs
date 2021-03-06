using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TravelTalkApi.Entities
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            Posts = new List<Post>();
            UpvotedPosts = new List<Post>();
        }

        // We need to define the associative Entity ourselves for the identity framework config
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Post> Posts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        [Required] public int NewNotifications { get; set; }

        // A list of the categories this user is mod to
        public virtual ICollection<Category>? CategoryMod { get; set; }

        public string? RefreshToken { get; set; }

        public ICollection<Post> UpvotedPosts { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}