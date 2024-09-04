using BlossomAvenue.Service.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class CreateUpdateUserDto
    {
        [Required, NameValidation]
        public string FirstName { get; set; } = null!;
        [Required, NameValidation]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
    }
}
