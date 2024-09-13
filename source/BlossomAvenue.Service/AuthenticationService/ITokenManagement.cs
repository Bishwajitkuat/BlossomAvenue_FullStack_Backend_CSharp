using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public interface ITokenManagement
    {
        string GenerateToken(User user);
        public RefreshToken GenerateRefreshToken(User user);
        public Guid? GetUserIdFromToken(string token);
    }
}
