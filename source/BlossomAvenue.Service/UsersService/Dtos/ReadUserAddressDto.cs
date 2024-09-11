using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class ReadUserAddressDto
    {
        public Guid UserAddressId { get; set; }
        public bool? DefaultAddress { get; set; }
        public ReadAddressDetailDto Address { get; set; }

        public ReadUserAddressDto(UserAddress userAddress)
        {
            UserAddressId = userAddress.UserAddressId;
            DefaultAddress = userAddress.DefaultAddress;
            Address = new ReadAddressDetailDto(userAddress.AddressDetail);
        }
    }

    public class ReadAddressDetailDto
    {
        public Guid AddressDetailId { get; set; }
        public string FullName { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string PostCode { get; set; }
        public string? AddressLine2 { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }


        public ReadAddressDetailDto(AddressDetail addressDetail)
        {
            AddressDetailId = addressDetail.AddressDetailId;
            FullName = addressDetail.FullName;
            AddressLine1 = addressDetail.AddressLine1;
            AddressLine2 = addressDetail.AddressLine2;
            PostCode = addressDetail.PostCode;
            City = addressDetail.City;
            Country = addressDetail.Country;
        }
    }
}