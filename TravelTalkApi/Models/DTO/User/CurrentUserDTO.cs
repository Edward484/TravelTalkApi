using System.Collections.Generic;
using System.Linq;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Models.DTO.User
{
    public class CurrentUserDTO
    {
        public CurrentUserDTO(Entities.User user)
        {
            Roles = user.UserRoles.Select(ur => ur.RoleId).ToList();
            UpVotes = user.UpvotedPosts.Select(post => post.PostId).ToList();
            Email = user.Email;
            Username = user.UserName;
            Posts = user.Posts.Select(post => post.PostId).ToList();
            Notifications = user.Notifications.ToList();
            NewNotifications = user.NewNotifications;
            CategoryMod = user.CategoryMod.Select(categ => categ.CategoryId).ToList();
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public List<int> Posts { get; set; }
        public List<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public List<int> Roles { get; set; }
        public List<int> UpVotes { get; set; }
        public List<int> CategoryMod { get; set; }
    }
}