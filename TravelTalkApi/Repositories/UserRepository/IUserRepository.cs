using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUsersByEmail(string email);
        Task<User?> GetByIdWithRoles(int id);

        Task<User?> GetByUsernameWithRoles(string username);

        // Get all the data for the user
        Task<User> GetByIdComplete(int id);
    }
}