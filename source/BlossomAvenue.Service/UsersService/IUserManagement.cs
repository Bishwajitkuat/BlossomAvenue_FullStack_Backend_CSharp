using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.UsersService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService
{
    public interface IUserManagement
    {
        public Task<List<User>> GetUsers(UsersQueryDto query);
        public Task<User> GetUser(Guid userId);
        public Task<bool> UpdateUser(Guid userId, UpdateUserDto updateUserDto);
        public Task<bool> UpdateUserProfile(UpdateDetailedUserDto updateDetailedUserDto);
        public Task<bool> DeleteUser(Guid userId);
        public Task<User> CreateProfile(User profile);
    }
}
