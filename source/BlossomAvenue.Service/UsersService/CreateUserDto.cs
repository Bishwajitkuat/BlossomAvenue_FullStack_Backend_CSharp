using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService
{
    public class CreateUserDto
    {
        [
            Required, 
            MinLength(3,ErrorMessage = "First name at least should be 3 characters"), 
            MaxLength(50, ErrorMessage = "First name should not exceed 50 characters")
        ]
        public string FirstName { get; set; } = null!;
        [
            Required,
            MinLength(3, ErrorMessage = "First name at least should be 3 characters"),
            MaxLength(50, ErrorMessage = "First name should not exceed 50 characters")
        ]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }
    }
}
