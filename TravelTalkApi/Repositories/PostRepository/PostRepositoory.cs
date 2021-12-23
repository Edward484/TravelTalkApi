using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class PostRepository:GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetByAuthorId(int authorId)
        {
            return await this._context.Posts.Where(post => post.AuthorId == authorId).ToListAsync();
        }

        public async Task<List<Post>> GetByTopicId(int topicId)
        {
            return await this._context.Posts.Where(post => post.TopicId == topicId).ToListAsync();

        }
    }
}