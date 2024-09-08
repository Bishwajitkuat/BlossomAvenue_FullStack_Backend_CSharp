using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class ReadUserAddressDto
    {
        public Guid AddressId { get; set; }
        public bool? DefaultAddress { get; set; }
        public ReadAddressDetailDto Address { get; set; }

        public ReadUserAddressDto(UserAddress userAddress)
        {
            AddressId = userAddress.AddressId;
            DefaultAddress = userAddress.DefaultAddress;
            Address = new ReadAddressDetailDto(userAddress.Address);
        }
    }

    public class ReadAddressDetailDto
    {
        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public ReadAddressDetailDto(AddressDetail addressDetail)
        {
            AddressLine1 = addressDetail.AddressLine1;
            AddressLine2 = addressDetail.AddressLine2;
            CityId = addressDetail.CityId;
            CityName = addressDetail.City.CityName;
        }
    }
}