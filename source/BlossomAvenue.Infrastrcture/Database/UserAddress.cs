using System;
using System.Collections.Generic;

namespace BlossomAvenue.Infrastrcture.Database;

public partial class UserAddress
{
    public Guid UserId { get; set; }

    public Guid AddressId { get; set; }

    public bool? DefaultAddress { get; set; }

    public virtual AddressDetail Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
