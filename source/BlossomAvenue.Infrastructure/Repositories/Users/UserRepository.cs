using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.UsersService;
using System.Linq;
using BlossomAvenue.Service.SharedDtos;
using BlossomAvenue.Service.UsersService.Dtos;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly BlossomAvenueDbContext _context;

        public UserRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }

        public async Task<User>? CreateUser(User user)
        {
            var savedUser = (await _context.Users.AddAsync(user)).Entity;
            if (await _context.SaveChangesAsync() > 0)
            {
                // saved entity does not includes nested relationship data particularly from 2nd level.
                var newUser = await GetUser(savedUser.UserId);
                return newUser;
            }
            return null;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return false;
            _context.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User?> GetUser(Guid userId)
        {
            return await _context.Users
                .Include(u => u.UserContactNumbers)
                .Include(u => u.UserCredential)
                .Include(u => u.UserAddresses)
                .ThenInclude(ua => ua.Address)
                .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<User>> GetUsers(UsersQueryDto userquery)
        {

            var query = _context.Users
                .Include(u => u.UserContactNumbers)
                .Include(u => u.UserCredential)
                .Include(u => u.UserAddresses)
                .ThenInclude(ua => ua.Address)
                .ThenInclude(a => a.City)
                .AsQueryable();

            // filter by user role
            if (userquery.UserRole.HasValue)
            {
                query = query.Where(u => u.UserRole == userquery.UserRole);
            }
            // filter by active status
            if (userquery.IsActive.HasValue)
            {
                query = query.Where(u => u.IsUserActive == userquery.IsActive);
            }
            // filter by search word
            if (!string.IsNullOrEmpty(userquery.Search))
            {
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(userquery.Search.ToLower()) ||
                    u.LastName.ToLower().Contains(userquery.Search.ToLower()) ||
                    u.Email.ToLower().Contains(userquery.Search.ToLower()));
            }

            var isAscending = userquery.OrderBy == OrderBy.ASC;

            query = userquery.OrderUserWith switch
            {
                UsersOrderWith.FirstName => isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                UsersOrderWith.LastName => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName),
                UsersOrderWith.CreatedAt => isAscending ? query.OrderBy(u => u.CreatedAt) : query.OrderByDescending(u => u.CreatedAt),
                _ => isAscending ? query.OrderBy(u => u.CreatedAt) : query.OrderByDescending(u => u.CreatedAt)
            };

            var users = await query
                .Skip((userquery.PageNo - 1) * userquery.PageSize)
                .Take(userquery.PageSize)
                .ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<bool> CheckUserExistsByEmail(string email)
        {
            return _context.Users.Where(s => s.Email == email).AnyAsync();
        }

        public Task<User?> GetUserByUsername(string username)
        {
            return _context.Users
                .Include(u => u.UserCredential)
                .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => (u.UserCredential.UserName == username) && (u.IsUserActive ?? false));
        }

        public Task<bool> CheckEmailExistsWithOtherUsers(Guid userId, string email)
        {
            //Check if email exists in the database and the email is not the same as the user's email
            return _context.Users.Where(s => s.Email == email && s.UserId != userId).AnyAsync();
        }
    }
}
