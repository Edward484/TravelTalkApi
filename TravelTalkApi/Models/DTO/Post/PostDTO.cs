using System;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Models.DTO.Post
{
    public class PostDTO
    {
        public PostDTO(Entities.Post post)
        {
            PostId = post.PostId;
            Author = post.Author;
            CreatedAt = post.CreatedAt;
            Content = post.Content;
            Topic = post.Topic;
            ImageURL = post.ImageURL;
            UpvoteCount = post.UpvoteCount;
        }

        public int PostId { get; set; }
        public Entities.User Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public Topic Topic { get; set; }
        public string ImageURL { get; set; }
        public int UpvoteCount { get; set; }
    }
}