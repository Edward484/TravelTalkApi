using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdWithRoles(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public Task<User> GetByIdComplete(int id)
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.Notifications).Include(u => u.Posts).Include(u => u.UpvotedPosts)
                .FirstAsync(u => u.Id.Equals(id));
        }

        public async Task<User> GetUsersByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}