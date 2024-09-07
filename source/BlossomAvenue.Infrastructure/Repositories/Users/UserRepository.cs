using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.UsersService;
using System.Linq;
using BlossomAvenue.Service.SharedDtos;
using BlossomAvenue.Service.UsersService.Dtos;

namespace BlossomAvenue.Infrastructure.Repositories.Users
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

        public async Task<List<User>> GetUsers(UsersQueryDto userquery)
        {

            var query = _context.Users
                .Include(u => u.UserRole)
                .AsQueryable();

            // if (userquery.UserRoleId.HasValue)
            // {
            //     query.Where(u => u.UserRoleId == userquery.UserRoleId);
            // }

            if (!string.IsNullOrEmpty(userquery.Search))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(userquery.Search) ||
                    u.LastName.Contains(userquery.Search) ||
                    u.Email.Contains(userquery.Search));
            }

            var isAscending = userquery.OrderBy == OrderBy.ASC;

            query = userquery.OrderUserWith switch
            {
                UsersOrderWith.FirstName => isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                UsersOrderWith.LastName => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName),
                _ => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName)
            };

            var users = await query
                .Skip((userquery.PageNo - 1) * userquery.PageSize)
                .Take(userquery.PageSize)
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

        public Task<User?> GetUserByUsername(string username)
        {
            return _context.Users
                .Include(u => u.UserCredential)
                .FirstOrDefaultAsync(u => (u.UserCredential.UserName == username) && (u.IsUserActive ?? false));
        }

        public Task<bool> CheckEmailExistsWithOtherUsers(Guid userId, string email)
        {
            //Check if email exists in the database and the email is not the same as the user's email
            return _context.Users.Where(s => s.Email == email && s.UserId != userId).AnyAsync();
        }
    }
}
