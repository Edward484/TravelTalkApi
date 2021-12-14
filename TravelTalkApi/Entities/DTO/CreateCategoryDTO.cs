using System.Collections.Generic;

namespace TravelTalkApi.Entities.DTO
{
    public class CreateCategoryDTO
    {
        public CreateCategoryDTO(List<Topic> topics, string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}