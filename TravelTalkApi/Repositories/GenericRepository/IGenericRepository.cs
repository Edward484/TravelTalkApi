using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelTalkApi.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
    {
        // Get Data

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        // Create

        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);

        // Update

        void Update(TEntity entity);

        // Delete

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        // Save
        Task<bool> SaveAsync();
    }
}


// IQueryable - tine minte query-ul catre db, NU datele; Cand accesam se face query catre db
// IEnumerable - la fel dar in memory
// List - tine minte datele; 