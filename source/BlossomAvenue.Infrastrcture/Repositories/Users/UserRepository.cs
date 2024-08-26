using BlossomAvenue.Core.Repositories.Users;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastrcture.Database;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly BlossomAvenueDbContext _context;

        public UserRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsers(int pageNo, int pageSize, Guid? userRoleId, string orderWith, string orderBy, string? search)
        {

            var query = _context.Users
                .Include(u => u.UserRole)
                .AsQueryable();

            if (userRoleId.HasValue) 
            {
                query.Where(u => u.UserRoleId == userRoleId);
            }

            if (!string.IsNullOrEmpty(search)) 
            {
                query = query.Where( u =>
                    u.FirstName.Contains(search) || 
                    u.LastName.Contains(search) ||
                    u.Email.Contains(search));
            }

            var isAscending = orderBy.Equals("ASC", StringComparison.OrdinalIgnoreCase);

            query = orderWith.ToLower() switch
            {
                "firstname" => isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                "rolename" => isAscending ? query.OrderBy(u => u.UserRole.UserRoleName) : query.OrderByDescending(u => u.UserRole.UserRoleName),
                _ => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName)
            };

            var users = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return users;
        }

        public void UpdateUser(Guid userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
