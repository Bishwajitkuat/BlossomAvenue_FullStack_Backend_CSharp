using BlossomAvenue.Core.Products;
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
        private readonly IJwtManagement _jwtService;

        public AuthManagement(IUserRepository userRepository, IJwtManagement jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<AuthenticationResultDto> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return new AuthenticationResultDto() { IsAuthenticated = false };
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthenticationResultDto() { IsAuthenticated = true, Token = token };
        }

        public void Logout(string token)
        {
            _jwtService.InvalidateToken(token);
        }


    }
}
