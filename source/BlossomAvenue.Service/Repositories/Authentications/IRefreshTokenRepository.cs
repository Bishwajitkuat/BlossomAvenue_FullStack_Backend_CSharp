using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.Repositories.Authentications
{
    public interface IRefreshTokenRepository
    {
        public Task<bool> DeleteRefreshToken(Guid userId, string token);
        public Task<bool> DeleteAllRefreshToken(Guid userId, string token);
        public Task<RefreshToken>? GetUserByRefreshToken(string token);
        public Task<bool> AddRefreshToken(RefreshToken refreshToken);
    }
}