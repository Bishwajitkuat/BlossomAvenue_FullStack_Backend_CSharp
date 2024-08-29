using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.AuthenticationService
{
    public class AuthenticationResultDto
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; } = null!;
    }
}
