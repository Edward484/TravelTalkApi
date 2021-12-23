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

            return this._context.Categories.Include("Topics").Where(categ => categ.CategoryId == id).FirstAsync();
        }
    }
}