using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories.GenericRepository;

namespace TravelTalkApi.Repositories.CategoryRepository
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