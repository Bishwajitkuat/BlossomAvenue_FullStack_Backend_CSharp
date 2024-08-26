using AutoMapper;
using BlossomAvenue.Core.Repositories.Users;
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
        public Task<UserDto> CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUser(Guid userId)
        {
            throw new NotImplementedException();
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
