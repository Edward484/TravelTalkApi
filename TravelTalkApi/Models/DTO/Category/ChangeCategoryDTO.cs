using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TravelTalkApi.Entities.DTO
{
    public class ChangeCategoryDTO
    {
        [JsonConstructor]
        public ChangeCategoryDTO(int categoryId, string newName)
        {
            CategoryId = categoryId;
            this.newName = newName;
        }

        public int CategoryId { get; set; }
        public string newName { get; set; }

    }
}