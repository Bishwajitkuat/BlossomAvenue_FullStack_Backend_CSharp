using System;
using System.Collections.Generic;

namespace BlossomAvenue.Infrastrcture.Database;

public partial class UserContactNumber
{
    public Guid UserId { get; set; }

    public string ContactNumber { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
