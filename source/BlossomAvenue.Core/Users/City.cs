using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Users;

public partial class City
{
    public Guid CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<AddressDetail> AddressDetails { get; set; } = new List<AddressDetail>();

}
