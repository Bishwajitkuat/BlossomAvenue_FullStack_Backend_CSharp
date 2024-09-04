using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Users;

public partial class UserCredential
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
