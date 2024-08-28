using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomAttributes
{
    public class NameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string propertyName = validationContext.DisplayName;

            if (value is not string name) return new ValidationResult($"{propertyName} is required");

            if (name.Length < 3) return new ValidationResult($"{propertyName} should be at least 3 characters");

            if (name.Length > 50) return new ValidationResult($"{propertyName} should not exceed 50 characters");

            return ValidationResult.Success;
        }
    }
}
