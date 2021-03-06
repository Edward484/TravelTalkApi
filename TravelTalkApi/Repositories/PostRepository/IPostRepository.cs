using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public interface IPostRepository:IGenericRepository<Post>
    {
        public Task<List<Post>> GetByAuthorId(int authorId);
        public Task<List<Post>> GetByTopicId(int topicId);
        public Task<Post> GetByIdAsync(int postId, bool expanded);
        public void UpdateContent(int id, string newContent);

    }
}