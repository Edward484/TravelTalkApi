using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTalkApi.Entities
{
    public class Category
    {
        public Category()
        {
            Topics = new List<Topic>();
        }

        public int CategoryId { get; set; }
        public ICollection<Topic> Topics { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Mods { get; set; }
    }
}