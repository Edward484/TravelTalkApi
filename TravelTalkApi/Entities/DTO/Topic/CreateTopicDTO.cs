using System.Collections.Generic;
using System.Linq;

namespace TravelTalkApi.Entities.DTO.Topic
{
    public class CreateTopicDTO
    {
        public CreateTopicDTO(string title, string description, int categoryId)
        {
            Title = title;
            Description = description;
            CategoryId = categoryId;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}