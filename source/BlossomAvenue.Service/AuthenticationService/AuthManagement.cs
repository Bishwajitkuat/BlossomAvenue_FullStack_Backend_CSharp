using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.InMemory;
using BlossomAvenue.Service.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public class AuthManagement : IAuthManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenManagement _tokenMgt;
        private readonly IPasswordHasher _passwordHasher;

        public AuthManagement(IUserRepository userRepository, ITokenManagement tokenMgt, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenMgt = tokenMgt;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResultDto> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return new AuthenticationResultDto() { IsAuthenticated = false };

            }

            if (_passwordHasher.VerifyPassword(password, user.UserCredential.Password, user.UserCredential.Salt))
            {
                var token = _tokenMgt.GenerateToken(user);
                return new AuthenticationResultDto { IsAuthenticated = true, Token = token };
            }
            return new AuthenticationResultDto() { IsAuthenticated = false };
        }

        public void Logout(string token)
        {
            _tokenMgt.InvalidateToken(token);
        }


    }
}
