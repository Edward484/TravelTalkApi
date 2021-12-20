using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelTalkApi.Entities.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO(Category category)
        {
            CategoryId = category.CategoryId;
            Topics = category.Topics.ToList();
            Name = category.Name;
        }

        public int CategoryId { get; set; }
        public List<Topic> Topics { get; set; }
        public string Name { get; set; }
    }
}