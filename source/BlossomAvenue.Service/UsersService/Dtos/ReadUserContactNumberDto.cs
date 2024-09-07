using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class ReadUserContactNumberDto
    {
        public Guid ContactNumberId { get; set; }
        public string ContactNumber { get; set; }

        public ReadUserContactNumberDto(UserContactNumber userContactNumber)
        {
            ContactNumberId = userContactNumber.ContactNumberId;
            ContactNumber = userContactNumber.ContactNumber;
        }

    }
}