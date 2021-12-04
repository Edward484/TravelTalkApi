using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories.GenericRepository;

namespace TravelTalkApi.Repositories.PostRepository
{
    public interface IPostRepository:IGenericRepository<Post>
    {
        public Task<List<Post>> GetByAuthorId(int authorId);
        public Task<List<Post>> GetByTopicId(int topicId);
    }
}