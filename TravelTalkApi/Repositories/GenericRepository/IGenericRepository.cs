using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelTalkApi.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        // Get Data

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        // Create

        void Create(TEntity entity);
        void CreateRange(ICollection<TEntity> entities);

        // Update

        void Update(TEntity entity);

        // Delete

        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);

        // Save
        Task<bool> SaveAsync();
    }
}


// IQueryable - tine minte query-ul catre db, NU datele; Cand accesam se face query catre db
// IEnumerable - la fel dar in memory
// List - tine minte datele; 