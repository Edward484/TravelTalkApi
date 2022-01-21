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

        public Task<Topic> GetByIdAsync(int id, bool expanded)
        {
            var query = this._context.Topics.Where(topic => topic.TopicId == id);
            return expanded ? query.Include("Posts").Include("Author").FirstAsync() : query.FirstAsync();
        }

        public async Task<List<User>> GetParticipantsAsync(int topicId)
        {
            var topic = await this._context.Topics.Where(topic => topic.TopicId == topicId)
                .Include(topic => topic.Posts)
                .ThenInclude(post => post.Author).FirstAsync();
            return topic.Posts.Select(post => post.Author).DistinctBy(user => user.Id).ToList();
        }
        
    }
}