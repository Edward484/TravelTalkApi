using System.Collections;
using System.Collections.Generic;

namespace TravelTalkApi.Entities
{
    public class Topic
    {
        public Topic()
        {
            Posts = new List<Post>();
        }
        public int TopicId { get; set; }
        public string Title { get; set; }
        
        public int AuthorId { get; set; }
        public User Author { get; set; }
        
        public ICollection<Post> Posts { get; set; }
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}