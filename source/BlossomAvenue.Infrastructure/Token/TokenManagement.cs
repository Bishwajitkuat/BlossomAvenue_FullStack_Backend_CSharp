using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.AuthenticationService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlossomAvenue.Infrastructure.Token.Jwt
{
    public class TokenManagement : ITokenManagement
    {
        private readonly JwtConfiguration _jwtConfigurations;

        public TokenManagement(IOptions<JwtConfiguration> jwtSettings)
        {
            _jwtConfigurations = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, String.Concat(user.FirstName, " ", user.LastName)),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("CartId", user.Cart.CartId.ToString()),
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


        public RefreshToken GenerateRefreshToken(User user)
        {
            return new RefreshToken
            {
                UserId = user.UserId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiredAt = DateTime.Now.AddDays(7),
            };
        }

        public Guid? GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return new Guid(userId.Value);
        }








    }
}
