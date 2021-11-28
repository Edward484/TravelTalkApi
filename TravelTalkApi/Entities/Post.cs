using System;

namespace TravelTalkApi.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public Topic Topic { get; set; }
        public string ImageURL { get; set; }
    }
}