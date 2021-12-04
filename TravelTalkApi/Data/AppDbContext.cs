using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }    
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> User { get; set; }   
        public DbSet<UserWithNotification> UserWithNotifications { get; set; }    

    }
}