using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public Guid UserRoleId { get; set; }

        public string UserRoleName { get; set; } = null!;

        public bool IsUserActive { get; set; }
    }
}
