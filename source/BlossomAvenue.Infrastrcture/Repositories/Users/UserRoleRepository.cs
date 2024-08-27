using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Users
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BlossomAvenueDbContext _context;

        public UserRoleRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<UserRole?> GetUserRoleByName(string roleName)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserRoleName == roleName);
        }
    }
}
