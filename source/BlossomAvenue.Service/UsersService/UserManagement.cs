using AutoMapper;
using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Service.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManagement(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ActiveInactiveUser(Guid userId, bool status)
        {
            var user = await _userRepository.GetUser(userId) ?? throw new RecordNotFoundException("User");
            
            user.IsUserActive = status;
            await _userRepository.UpdateUser(user);
        }

        public Task<UserDto> CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailedDto> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);
            return user is null ? throw new RecordNotFoundException("User") : _mapper.Map<UserDetailedDto>(user);
        }

        public async Task<List<UserDto>> GetUsers(int pageNo, int pageSize, Guid? userRoleId, string orderWith, string orderBy, string? search)
        {
            var users = await _userRepository.GetUsers(pageNo, pageSize, userRoleId, orderWith, orderBy, search);
            return _mapper.Map<List<UserDto>>(users);
        }

        public void UpdateUser(Guid userId, UserDto user)
        {

            throw new NotImplementedException();
        }
    }
}
