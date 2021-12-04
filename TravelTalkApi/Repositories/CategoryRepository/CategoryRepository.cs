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
    }
}