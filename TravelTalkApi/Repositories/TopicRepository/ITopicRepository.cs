using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public interface ITopicRepository:IGenericRepository<Topic>
    {
        public Task<List<Topic>> GetByAuthorId(int authorId);
        public Task<List<Topic>> GetByCategoryId(int categoryId);
    }
}