using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UsersService
{
    public class AddressDto
    {
        public Guid AddressId { get; set; }
        public bool IsDefaultAddress { get; set; } = false;
        public string AddressLine1 { get; set; } = String.Empty;
        public string? AddressLine2 { get; set; } = null!;
        public string CityName { get; set; } = String.Empty;
        public Guid? CityId { get; set; }
    }
}
