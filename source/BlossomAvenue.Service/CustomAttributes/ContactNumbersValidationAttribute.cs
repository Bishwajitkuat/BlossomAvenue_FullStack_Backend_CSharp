using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomAttributes
{
    public class ContactNumbersValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string[] contactNumbers || contactNumbers.Length == 0)
            {
                return new ValidationResult("At least one contact number is required.");
            }

            if (!contactNumbers.Any(num => num.Length == 10))
            {
                return new ValidationResult("At least one contact number must contain exactly 10 characters.");
            }

            return ValidationResult.Success;
        }
    }
}
