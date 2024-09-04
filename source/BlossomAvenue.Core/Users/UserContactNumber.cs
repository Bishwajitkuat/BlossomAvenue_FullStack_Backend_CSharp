using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Users;

public partial class UserContactNumber
{
    public Guid UserId { get; set; }

    public string ContactNumber { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
