using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomAttributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string password) return new ValidationResult("Password is required");

            if (password.Length < 8) return new ValidationResult("Password should be at least 8 characters");

            if (!password.Any(char.IsDigit)) return new ValidationResult("Password should contain at least one digit");

            if (!password.Any(char.IsUpper)) return new ValidationResult("Password should contain at least one uppercase letter");

            if (!password.Any(char.IsLower)) return new ValidationResult("Password should contain at least one lowercase letter");

            string specialCharacters = @"!@#$%^&*()-_=+[{]}\|;:'"",<.>/?";
            if (!password.Any(ch => specialCharacters.Contains(ch)))
                return new ValidationResult("Password should contain at least one special character");

            return ValidationResult.Success;
        }
    }
}
