using System;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Data
{
    public class AppDbContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> User { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "travelTalk.db");
        }
    }
}