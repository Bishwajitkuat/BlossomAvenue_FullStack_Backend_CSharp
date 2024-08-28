using BlossomAvenue.Service.Repositories.Users;
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

        public async Task<User> CreateUser(User user)
        {
            var savedUser = (await _context.Users.AddAsync(user)).Entity;
            _context.SaveChanges();
            return savedUser;
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUser(Guid userId)
        {
            return _context.Users
                .Include(u => u.UserRole)
                .Include(u => u.UserContactNumbers)
                .Include(u => u.UserAddresses)
                .ThenInclude(ua => ua.Address)
                .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(u => u.UserId == userId);
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
                "firstName" => isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                "roleName" => isAscending ? query.OrderBy(u => u.UserRole.UserRoleName) : query.OrderByDescending(u => u.UserRole.UserRoleName),
                _ => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName)
            };

            var users = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return users;
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> CheckUserExistsByEmail(string email)
        {
            return _context.Users.Where(s => s.Email == email).AnyAsync();
        }

        public Task<User?> GetUserByUsernameAndPassword(string username) 
        {
            return _context.Users
                .Include(u => u.UserRole)
                .Include(u => u.UserCredential)
                .FirstOrDefaultAsync(u => u.UserCredential.UserName == username);
        }
    }
}
