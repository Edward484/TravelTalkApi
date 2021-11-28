using System.Collections;
using System.Collections.Generic;

namespace TravelTalkApi.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public string Name { get; set; }
    }
}