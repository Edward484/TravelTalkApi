using System.Text.Json.Serialization;

namespace TravelTalkApi.Models.DTO.Post
{
    public class ChangePostContentDTO
    {
        [JsonConstructor]
        public ChangePostContentDTO(int postId, string content )
        {
            PostId = postId;
            Content = content;
        }

        public string Content { get; set; }
        public int PostId { get; set; }
        
    }
}