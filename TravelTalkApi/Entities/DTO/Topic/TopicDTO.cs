using System.Collections.Generic;
using System.Linq;

namespace TravelTalkApi.Entities.DTO
{
    public class TopicDTO
    {
        public TopicDTO(Entities.Topic topic)
        {
            TopicId = topic.TopicId;
            Title = topic.Title;
            Author = topic.Author;
            Posts = topic.Posts.ToList();
            Description =topic.Description;
            Category = topic.Category;
        }

        public int TopicId { get; set; }
        public string Title { get; set; }

        public User Author { get; set; }

        public List<Post> Posts { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
    }
}