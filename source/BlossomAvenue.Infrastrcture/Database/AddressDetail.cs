using System;
using System.Collections.Generic;

namespace BlossomAvenue.Infrastrcture.Database;

public partial class AddressDetail
{
    public Guid AddressId { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public Guid? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
