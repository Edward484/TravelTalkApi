using System;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Models.DTO.Post
{
    public class PostDTO
    {
        public PostDTO(Entities.Post post)
        {
            try
            {
                PostId = post.PostId;
                AuthorId = post.Author.Id;
                AuthorName = post.Author.UserName;
                CreatedAt = ((DateTimeOffset) post.CreatedAt).ToUnixTimeSeconds();
                Content = post.Content;
                TopicId = post.TopicId;
                ImageURL = post.ImageURL;
                UpvoteCount = post.UpvoteCount;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public long CreatedAt { get; set; }
        public string Content { get; set; }
        public int TopicId { get; set; }
        public string ImageURL { get; set; }
        public int UpvoteCount { get; set; }
    }
}