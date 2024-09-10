﻿using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.CustomAttributes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class CreateUserProfileDto
    {

        [Required, NameValidation]
        public string FirstName { get; set; } = null!;

        [Required, NameValidation]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [ContactNumbersValidation]
        public string[] ContactNumbers { get; set; } = [];

        [Required]
        public string AddressLine1 { get; set; } = null!;

        public string AddressLine2 { get; set; } = null!;
        public Guid? CityId { get; set; }
        [Required]
        public string CityName { get; set; }

        [Required, PasswordValidation]
        public string Password { get; set; } = null!;

        [Required, MinLength(5)]
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
                        Address = new AddressDetail{
                            AddressLine1 = AddressLine1,
                            AddressLine2 = AddressLine2,
                            //CityId = CityId,
                        }
                    }
                },
            };

            if (CityId != null)
            {
                newUser.UserAddresses.ToList()[0].Address.CityId = (Guid)CityId;
            }
            else
            {
                newUser.UserAddresses.ToList()[0].Address.City = new City { CityName = CityName };
            }

            return newUser;

        }
    }
}
