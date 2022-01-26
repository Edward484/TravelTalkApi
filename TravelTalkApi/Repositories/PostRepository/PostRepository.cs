using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
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
            return await this._context.Posts.Include(post => post.Author).Where(post => post.TopicId == topicId).ToListAsync();
        }

        public Task<Post> GetByIdAsync(int postId, bool expanded)
        {
            var query = this._context.Set<Post>().Where(post => post.PostId == postId);
            return expanded ? query.Include("Author").FirstAsync() : query.FirstAsync();
        }

        public async void UpdateContent(int id, string newContent)
        {
            var post = await _context.Posts.Where(p => p.PostId == id).FirstOrDefaultAsync();
            post.Content = newContent;
        }
    }
}