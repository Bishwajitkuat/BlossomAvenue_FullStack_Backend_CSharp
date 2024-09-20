using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class CreateUserProfileDto
    {

        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string[] ContactNumbers { get; set; } = [];


        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }


        public string PostCode { get; set; } = null!;


        public string City { get; set; } = null!;

        public Country Country { get; set; }


        public string Password { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public User ConvertToUser()
        {
            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                UserCredential = new UserCredential
                {
                    UserName = UserName,
                    Password = Password,
                },
                IsUserActive = true,
                UserContactNumbers = ContactNumbers.Select(c => new UserContactNumber { ContactNumber = c }).ToList(),
                UserAddresses = new List<UserAddress>
                {
                    new UserAddress{
                        DefaultAddress = true,
                        AddressDetail = new AddressDetail{
                            FullName = $"{FirstName} {LastName}",
                            AddressLine1 = AddressLine1,
                            AddressLine2 = AddressLine2,
                            PostCode = PostCode,
                            City = City,
                            Country = Country,
                        }
                    }
                },
                Cart = new Cart()
            };

            return newUser;

        }
    }
}
