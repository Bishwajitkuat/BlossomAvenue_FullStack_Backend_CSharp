using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UpdateUserDto
    {
        public BlossomAvenue.Core.ValueTypes.UserRole UserRole { get; set; }
        public bool IsUserActive { get; set; }

        public User UpdateUser(User user)
        {
            user.UserRole = UserRole;
            user.IsUserActive = IsUserActive;
            return user;
        }
    }
}