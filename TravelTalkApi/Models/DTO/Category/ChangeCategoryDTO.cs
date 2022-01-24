using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelTalkApi.Entities.DTO
{
    public class ChangeCategoryDTO
    {
        public ChangeCategoryDTO(Category category)
        {
            CategoryId = category.CategoryId;
            newName = category.Name;

        }

        public int CategoryId { get; set; }
        public string newName { get; set; }

    }
}