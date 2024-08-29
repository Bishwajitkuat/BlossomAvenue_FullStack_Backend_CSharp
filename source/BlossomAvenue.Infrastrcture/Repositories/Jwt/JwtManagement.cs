using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.AuthenticationService;
using BlossomAvenue.Service.Repositories.InMemory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlossomAvenue.Infrastrcture.Repositories.Jwt
{
    public class JwtManagement : IJwtManagement
    {
        private readonly JwtConfiguration _jwtConfigurations;
        private readonly IInMemoryDB _inMemoryDB;

        public JwtManagement(IOptions<JwtConfiguration> jwtSettings, IInMemoryDB inMemoryDB)
        {
            _jwtConfigurations = jwtSettings.Value;
            this._inMemoryDB = inMemoryDB;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, String.Concat(user.FirstName, " ", user.LastName)),
                new Claim(ClaimTypes.Role, user.UserRole.UserRoleName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfigurations.Issuer,
                audience: _jwtConfigurations.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigurations.ExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void InvalidateToken(string token)
        {
            _inMemoryDB.AddDeniedToken(token);
        }

        public bool ValidateToken(string token)
        {
            var deniedTokens = _inMemoryDB.GetDeniedTokens();

            return !deniedTokens.Contains(token);
        }
    }
}
