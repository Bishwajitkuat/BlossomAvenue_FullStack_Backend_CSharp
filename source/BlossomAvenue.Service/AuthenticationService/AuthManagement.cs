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

        public async Task<AuthenticationResultDto> GetRefreshToken(string jwtToken, string refreshToken)
        {
            var response = new AuthenticationResultDto();
            response.LoginResponseDto = new LoginResponseDto();
            response.LoginResponseDto.IsAuthenticated = false;
            // get user id from jwt token
            var userId = _tokenMgt.GetUserIdFromToken(jwtToken);

            if (userId != null)
            {
                // find the refresh token by refresh token
                var oldRefreshToken = await _refreshTokenRepository.GetUserByRefreshToken(refreshToken);
                if (oldRefreshToken == null)
                {
                    return response;
                }
                else if (oldRefreshToken.UserId != userId)
                {
                    // delete the refresh token, it might be compromised 
                    var res = await _refreshTokenRepository.DeleteRefreshTokenByRefreshToken(refreshToken);
                    return response;
                }
                else if (oldRefreshToken.UserId == userId && oldRefreshToken.ExpiredAt > DateTime.Now)
                {
                    // create new jwt token
                    var newToken = _tokenMgt.GenerateToken(oldRefreshToken.User);

                    // generate refresh token
                    var newRefreshToken = _tokenMgt.GenerateRefreshToken(oldRefreshToken.User);
                    // update the token in db
                    oldRefreshToken.Token = newRefreshToken.Token;
                    oldRefreshToken.CreatedAt = DateTime.Now;
                    oldRefreshToken.ExpiredAt = newRefreshToken.ExpiredAt;
                    var isSaved = await _refreshTokenRepository.UpdateRefreshToken(oldRefreshToken);
                    if (!isSaved) throw new RecordNotUpdatedException("refresh token");
                    // update the response objet with appropriate values
                    response.LoginResponseDto.IsAuthenticated = true;
                    response.LoginResponseDto.Token = newToken;
                    response.LoginResponseDto.UserRole = oldRefreshToken.User.UserRole;
                    response.RefreshToken = newRefreshToken;
                    // return the response
                    return response;
                }
            }
            return response;
        }


        {
            _tokenMgt.InvalidateToken(token);
        }


    }
}
