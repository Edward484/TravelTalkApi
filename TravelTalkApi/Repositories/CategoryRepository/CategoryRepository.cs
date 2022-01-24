using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Category> GetByIdAsync(int id)
        {
            //Include the topics in the query

            return this._context.Categories.Include(category => category.Topics).ThenInclude(topic => topic.Author)
                .Where(categ => categ.CategoryId == id).FirstAsync();
        }

        public Task<List<Category>> GetAll(bool expanded)
        {
            var categoriesQuery = this._context.Set<Category>().AsNoTracking();
            return
                expanded ? categoriesQuery.Include("Topics").ToListAsync() : categoriesQuery.ToListAsync();
        }

        public Task<Category> GetByIdWithModsAsync(int id)
        {
            return this._context.Set<Category>().Where(category => category.CategoryId == id).Include("Mods")
                .FirstAsync();
        }

        public async Task<Category> GetByTopicId(int id)
        {
            var res = await this._context.Set<Topic>().Where(topic => topic.TopicId == id).Include("Category")
                .FirstOrDefaultAsync();
            return res.Category;
        }

        public async void UpdateName(int id, string newName)
        {
            var post = await _context.Categories.Where(p => p.CategoryId == id).FirstOrDefaultAsync();
            if (post != null)
            {
                post.Name = newName;
            }
        }
    }
}