using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public interface IJwtManagement
    {
        string GenerateToken(User user);
        void InvalidateToken(string token);
        bool ValidateToken(string token);
    }
}
