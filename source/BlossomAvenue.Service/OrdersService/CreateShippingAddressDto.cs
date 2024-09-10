using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.OrdersService
{
    public class CreateShippingAddressDto
    {

        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public Guid? CityId { get; set; }
        [Required]
        public string CityName { get; set; }

        public AddressDetail ConvertToAddressDetail()
        {
            var address = new AddressDetail
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
            };

            if (CityId != null)
            {
                address.CityId = (Guid)CityId;
            }
            else
            {
                // new city entry will be created.
                address.City = new City { CityName = CityName };
            }
            return address;

        }
    }
}