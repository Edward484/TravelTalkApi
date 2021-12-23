using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Topic>> GetByAuthorId(int authorId)
        {
            return await this._context.Topics.Where(topic => topic.AuthorId == authorId).ToListAsync();
        }

        public async Task<List<Topic>> GetByCategoryId(int categoryId)
        {
            return await this._context.Topics.Where(topic => topic.CategoryId == categoryId).ToListAsync();
        }
    }
}