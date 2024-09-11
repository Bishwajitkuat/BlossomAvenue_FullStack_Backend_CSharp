using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlossomAvenue.Core.Users;

public partial class UserAddress
{
    public Guid UserAddressId { get; set; }

    public Guid UserId { get; set; }
    public Guid AddressDetailId { get; set; }

    public bool? DefaultAddress { get; set; }

    public virtual AddressDetail AddressDetail { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
