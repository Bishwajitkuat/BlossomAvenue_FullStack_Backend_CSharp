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
        public Task<UserDto> CreateUser(Dtos.CreateUpdateUserDto user);
        public Task<List<UserDto>> GetUsers(UsersQueryDto query);
        public Task<User> GetUser(Guid userId);
        public Task<bool> UpdateUser(Guid userId, UpdateUserDto updateUserDto);
        public Task<bool> UpdateUserProfile(UpdateDetailedUserDto updateDetailedUserDto);
        public void DeleteUser(Guid userId);
        public Task ActiveInactiveUser(Guid userId, bool status);
        public Task<User> CreateProfile(User profile);
    }
}
