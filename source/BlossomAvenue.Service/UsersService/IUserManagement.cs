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
        public Task<UserDetailedDto> GetUser(Guid userId);
        public Task UpdateUser(Guid userId, CreateUpdateUserDto user);
        public void DeleteUser(Guid userId);
        public Task ActiveInactiveUser(Guid userId, bool status);
        public Task<CreateDetailedUserResponseDto> CreateProfile(CreateDetailedUserDto profile);
    }
}
