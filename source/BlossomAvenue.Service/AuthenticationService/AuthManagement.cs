using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Authentications;
using BlossomAvenue.Service.Repositories.InMemory;
using BlossomAvenue.Service.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public class AuthManagement : IAuthManagement
    {
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenManagement _tokenMgt;
        private readonly IPasswordHasher _passwordHasher;

        public AuthManagement(IUserRepository userRepository, ITokenManagement tokenMgt, IPasswordHasher passwordHasher, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenMgt = tokenMgt;
            _passwordHasher = passwordHasher;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<AuthenticationResultDto> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return new AuthenticationResultDto { LoginResponseDto = new LoginResponseDto() { IsAuthenticated = false }, RefreshToken = null };
            }

            if (_passwordHasher.VerifyPassword(password, user.UserCredential.Password, user.UserCredential.Salt))
            {
                var token = _tokenMgt.GenerateToken(user);
                // generate refresh token
                var refreshToken = _tokenMgt.GenerateRefreshToken(user);
                // save refresh token in db
                var isSaved = await _refreshTokenRepository.AddRefreshToken(refreshToken);
                if (isSaved)
                {
                    return new AuthenticationResultDto
                    {
                        LoginResponseDto = new LoginResponseDto { IsAuthenticated = true, UserRole = user.UserRole, Token = token },
                        RefreshToken = refreshToken
                    };
                }
                else throw new Exception("Failed to save the refresh token in the db");
            }
            return new AuthenticationResultDto { LoginResponseDto = new LoginResponseDto() { IsAuthenticated = false }, RefreshToken = null };
        }


        public void Logout(string token)
        {
            _tokenMgt.InvalidateToken(token);
        }


    }
}
