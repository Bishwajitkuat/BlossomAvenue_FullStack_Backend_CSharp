using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class ReadDetailedUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public ICollection<ReadUserContactNumberDto> UserContactNumbers { get; set; }
        public ICollection<ReadUserAddressDto> UserAddresses { get; set; }

        public ReadDetailedUserDto(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserCredential.UserName;
            UserContactNumbers = user.UserContactNumbers.Select(cn => new ReadUserContactNumberDto(cn)).ToList();
            UserAddresses = user.UserAddresses.Select(a => new ReadUserAddressDto(a)).ToList();
        }

    }
}