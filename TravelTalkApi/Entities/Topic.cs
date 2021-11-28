using System.Collections;
using System.Collections.Generic;

namespace TravelTalkApi.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public User Author { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
    }
}