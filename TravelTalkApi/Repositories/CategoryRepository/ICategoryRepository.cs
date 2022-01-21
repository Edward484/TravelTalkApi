using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        public Task<List<Category>> GetAll(bool expanded);
        public Task<Category> GetByIdWithModsAsync(int id);
        public Task<Category> GetByTopicId(int id);
        public void UpdateName(int id, string newName);


    }
}