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
        public AddressDetail Address { get; set; }

        public ReadUserAddressDto(UserAddress userAddress)
        {
            AddressId = userAddress.AddressId;
            DefaultAddress = userAddress.DefaultAddress;
            Address = userAddress.Address;
        }
    }
}