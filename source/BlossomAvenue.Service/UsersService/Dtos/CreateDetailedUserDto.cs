using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Service.CustomAttributes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class CreateDetailedUserDto
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
        [Required]
        public Guid CityId { get; set; }

        [Required, PasswordValidation]
        public string Password { get; set; } = null!;

        [Required, MinLength(5)]
        public string UserName { get; set; } = null!;
    }
}
