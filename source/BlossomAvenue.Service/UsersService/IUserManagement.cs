using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService
{
    public interface IUserManagement
    {
        public Task<UserDto> CreateUser(CreateUserDto user);
        public Task<List<UserDto>> GetUsers(int pageNo, int pageSize, Guid? userRoleId, string orderWith, string orderBy, string? search);
        public Task<UserDetailedDto> GetUser(Guid userId);
        public void UpdateUser(Guid userId, UserDto user);
        public void DeleteUser(Guid userId);
        public Task ActiveInactiveUser(Guid userId, bool status);
        public Task<CreateDetailedUserResponseDto> CreateProfile(CreateDetailedUserDto profile);
    }
}
