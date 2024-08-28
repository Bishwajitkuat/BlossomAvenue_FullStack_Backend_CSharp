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
using BlossomAvenue.Service.Repositories.Cities;

namespace BlossomAvenue.Service.UsersService
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserManagement(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            ICityRepository cityRepository,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public UserManagement(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ActiveInactiveUser(Guid userId, bool status)
        {

            var user = await _userRepository.GetUser(userId) ?? throw new RecordNotFoundException(typeof(User).Name);

            user.IsUserActive = status;
            await _userRepository.UpdateUser(user);
        }


        public async Task<UserDto> CreateUser(CreateUserDto user)
        {
            if (await _userRepository.CheckUserExistsByEmail(user.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);

            var adminUserRole = await GetAdminRole();

            var userEntity = _mapper.Map<User>(user);
            userEntity.UserId = Guid.Empty;
            userEntity.UserRole = adminUserRole;
            userEntity.IsUserActive = true;

            var createdUser = await _userRepository.CreateUser(userEntity);

            //TODO: Send email to user with password

            return _mapper.Map<UserDto>(createdUser);
        }

            public async Task<CreateDetailedUserResponseDto> CreateProfile(CreateDetailedUserDto profile)
            {
                if (await _userRepository.CheckUserExistsByEmail(profile.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);

                if (!(await _cityRepository.IsCityExists(profile.CityId))) throw new RecordNotFoundException(typeof(City).Name);

                var userRole = await GetUserRole();

                var userEntity = _mapper.Map<User>(profile);
                userEntity.UserRole = userRole;
                var savedUser = await _userRepository.CreateUser(userEntity);
                var returnProfile = _mapper.Map<CreateDetailedUserResponseDto>(profile);
                returnProfile.UserId = savedUser.UserId;
                returnProfile.Password = String.Empty;

                return returnProfile;
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

                return await GetRole(adminRoleName);
            }

            private async Task<UserRole> GetUserRole()
            {
                var adminRoleName = _configuration.GetSection("UserRoles").GetSection("User").Value;
                if (string.IsNullOrEmpty(adminRoleName))
                {
                    throw new RecordNotFoundException("User Role in config");
                }

                return await GetRole(adminRoleName);
            }

            private async Task<UserRole> GetRole(string roleName)
            {
                var userRole = await _userRoleRepository.GetUserRoleByName(roleName);
                return userRole is null ? throw new RecordNotFoundException("User Role") : userRole;
            }
        }
    }

