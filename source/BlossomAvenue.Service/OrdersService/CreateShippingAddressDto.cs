using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.OrdersService
{
    public class CreateShippingAddressDto
    {
        [Required, MinLength(2, ErrorMessage = "Name must be 2 characters long")]
        public string FullName { get; set; } = null!;
        [Required]
        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        [Required, StringLength(5, ErrorMessage = "Invalid formate, a valid postcode has five digits.")]
        public string PostCode { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;
        [Required]
        public Country Country { get; set; }

        public AddressDetail ConvertToAddressDetail()
        {
            return new AddressDetail
            {
                FullName = FullName,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                PostCode = PostCode,
                City = City,
                Country = Country,
            };

        }
    }
}