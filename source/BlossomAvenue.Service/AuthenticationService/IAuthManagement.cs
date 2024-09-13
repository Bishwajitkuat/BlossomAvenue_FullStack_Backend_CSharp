using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public interface IAuthManagement
    {
        public Task<AuthenticationResultDto> Login(string username, string password);
        public Task<AuthenticationResultDto> GetRefreshToken(string jwtToken, string refreshToken);
        public Task<bool> Logout(string jwtToken, string refreshToken);
    }
}
