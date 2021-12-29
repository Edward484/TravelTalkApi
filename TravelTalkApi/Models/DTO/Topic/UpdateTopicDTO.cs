namespace TravelTalkApi.Entities.DTO
{
    public class UpdateTopicDTO
    {
        public UpdateTopicDTO(string description)
        {
            Description = description;
        }
       public string Description { get; set; }
    }
}