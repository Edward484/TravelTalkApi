namespace TravelTalkApi.Models.DTO.Post
{
    public class CreatePostDTO
    {
        public string Content { get; set; }
        public int TopicId { get; set; }
        public string ImageURL { get; set; }
    }
}