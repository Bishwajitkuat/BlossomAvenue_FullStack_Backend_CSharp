using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Core.Authentication
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public User User { get; set; } = null!;
    }
}