using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Service.Repositories.Authentications;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastructure.Repositories.RefreshTokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private BlossomAvenueDbContext _context;

        public RefreshTokenRepository(BlossomAvenueDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
        {
            var token = _context.RefreshTokens.Add(refreshToken);
            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<bool> DeleteAllRefreshToken(Guid userId, string token)
        {
            var validToken = _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.Token == token);
            if (validToken != null)
            {
                // token belongs to the correct user, so deleting all the tokens belongs to the user
                var tokens = _context.RefreshTokens.Where(t => t.UserId == userId);
                _context.RefreshTokens.RemoveRange(tokens);
                if (await _context.SaveChangesAsync() > 0) return true;
            }
            return false;
        }

        public async Task<bool> DeleteRefreshToken(Guid userId, string token)
        {
            var validToken = _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.Token == token);
            if (validToken != null)
            {
                _context.Remove(validToken);
                if (await _context.SaveChangesAsync() > 0) return true;
            }
            return false;
        }

        public async Task<RefreshToken>? GetUserByRefreshToken(string token)
        {
            var tokenWithUser = await _context.RefreshTokens
                                    .Include(t => t.User)
                                    .FirstOrDefaultAsync(t => t.Token == token);

            return tokenWithUser;
        }




    }
}