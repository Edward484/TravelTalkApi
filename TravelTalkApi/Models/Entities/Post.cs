using System;
using System.ComponentModel.DataAnnotations;

namespace TravelTalkApi.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        
        [Required]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string Content { get; set; }
        
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
        public string ImageURL { get; set; }
        
        [Required]
        public int UpvoteCount { get; set; }
    }
}