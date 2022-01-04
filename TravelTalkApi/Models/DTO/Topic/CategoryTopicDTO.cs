namespace TravelTalkApi.Entities.DTO
{
    public class CategoryTopicDTO
    {
        public CategoryTopicDTO(Entities.Topic topic)
        {
            TopicId = topic.TopicId;
            Title = topic.Title;
            Author = topic.Author;
            Description =topic.Description;
            CategoryId = topic.CategoryId;
        }

        public int TopicId { get; set; }
        public string Title { get; set; }

        public User Author { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}