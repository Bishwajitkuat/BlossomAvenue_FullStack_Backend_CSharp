using AutoMapper;
using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Service.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using Microsoft.Extensions.Configuration;

namespace BlossomAvenue.Service.UsersService
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserManagement(
            IUserRepository userRepository, 
            IUserRoleRepository userRoleRepository, 
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task ActiveInactiveUser(Guid userId, bool status)
        {
            var user = await _userRepository.GetUser(userId) ?? throw new RecordNotFoundException(typeof(User).Name);
            
            user.IsUserActive = status;
            await _userRepository.UpdateUser(user);
        }

        public async Task<UserDto> CreateUser(CreateUserDto user)
        {
            if(await _userRepository.CheckUserExistsByEmail(user.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);

            var adminUserRole = await GetAdminRole();  

            var userEntity = _mapper.Map<User>(user);
            userEntity.UserId = Guid.Empty;
            userEntity.UserRole = adminUserRole;
            userEntity.IsUserActive = true;

            var createdUser = await _userRepository.CreateUser(userEntity);
            return _mapper.Map<UserDto>(createdUser);
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

        private async Task<UserRole> GetAdminRole()
        {
            var adminRoleName = _configuration.GetSection("UserRoles").GetSection("Admin").Value;

            if (string.IsNullOrEmpty(adminRoleName))
            {
                throw new RecordNotFoundException("Admin Role in config");
            }

            var userRole = await _userRoleRepository.GetUserRoleByName(adminRoleName);

            return userRole is null ? throw new RecordNotFoundException("User Role") : userRole;
        }
    }
}
