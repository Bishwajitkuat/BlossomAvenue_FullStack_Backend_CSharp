using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.AuthenticationService
{
    public class LoginResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public UserRole UserRole { get; set; }
        public string Token { get; set; }

    }
}