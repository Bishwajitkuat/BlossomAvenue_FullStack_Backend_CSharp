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
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Service.UsersService.Dtos;
using System.Security.Cryptography;

namespace BlossomAvenue.Service.UsersService
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public UserManagement(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            ICityRepository cityRepository,
            IMapper mapper,
            IConfiguration configuration,
            IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _configuration = configuration;
            this._passwordHasher = passwordHasher;
        }

        public UserManagement(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<User> CreateProfile(User profile)
        {
            if (await _userRepository.CheckUserExistsByEmail(profile.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);
            _passwordHasher.HashPassword(profile.UserCredential.Password, out string hashedPassword, out byte[] salt);
            profile.UserCredential.Password = hashedPassword;
            profile.UserCredential.Salt = salt;
            var savedUser = await _userRepository.CreateUser(profile);
            return savedUser;
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);
            return user is null ? throw new RecordNotFoundException("User") : user;
        }

        public async Task<List<UserDto>> GetUsers(UsersQueryDto query)
        {
            var users = await _userRepository.GetUsers(query);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<bool> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            var existing = await _userRepository.GetUser(userId) ?? throw new RecordNotFoundException(typeof(User).Name);
            var updatedUser = updateUserDto.UpdateUser(existing);
            var updateStatus = await _userRepository.UpdateUser(updatedUser);
            if (!updateStatus) throw new RecordNotUpdatedException("User");
            return updateStatus;

        }

        public async Task<bool> UpdateUserProfile(UpdateDetailedUserDto updateDetailedUserDto)
        {
            var existing = await _userRepository.GetUser(updateDetailedUserDto.UserId) ?? throw new RecordNotFoundException(typeof(User).Name);
            var updatedUser = updateDetailedUserDto.UpdateUser(existing);
            var updateStatus = await _userRepository.UpdateUser(updatedUser);
            if (!updateStatus) throw new RecordNotUpdatedException("profile");
            return updateStatus;
        }


    }
}

