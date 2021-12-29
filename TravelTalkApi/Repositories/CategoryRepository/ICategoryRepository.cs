using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        public Task<List<Category>> GetAll(bool expanded);
        public Task<Category> GetByIdWithModesAsync(int id);
    }
}