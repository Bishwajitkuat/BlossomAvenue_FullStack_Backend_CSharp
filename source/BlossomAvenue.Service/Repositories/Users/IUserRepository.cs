using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.UsersService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task<List<User>> GetUsers(UsersQueryDto userquery);
        public Task<User?> GetUser(Guid userId);
        public Task UpdateUser(User user);
        public void DeleteUser(Guid userId);
        public Task<bool> CheckUserExistsByEmail(string email);
        public Task<bool> CheckEmailExistsWithOtherUsers(Guid userId, string email);
        public Task<User?> GetUserByUsername(string username);
    }
}
