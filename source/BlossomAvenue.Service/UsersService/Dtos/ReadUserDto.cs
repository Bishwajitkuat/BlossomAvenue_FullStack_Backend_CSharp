using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class ReadUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserRole UserRole { get; set; }
        public bool? IsUserActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<ReadUserContactNumberDto> UserContactNumbers { get; set; }
        public ICollection<ReadUserAddressDto> UserAddresses { get; set; }

        public ReadUserDto(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserCredential.UserName;
            UserRole = user.UserRole;
            IsUserActive = user.IsUserActive;
            CreatedAt = user.CreatedAt;
            UserContactNumbers = user.UserContactNumbers.Select(cn => new ReadUserContactNumberDto(cn)).ToList();
            UserAddresses = user.UserAddresses.Select(a => new ReadUserAddressDto(a)).ToList();
        }
    }
}