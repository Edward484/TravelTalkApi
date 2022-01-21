using System.Threading.Tasks;
using TravelTalkApi.Models.DTO.Post;

namespace TravelTalkApi.Services.PostService
{
    public interface IPostService
    {
        public Task<PostDTO> CreatePostService(CreatePostDTO body);

    }
}