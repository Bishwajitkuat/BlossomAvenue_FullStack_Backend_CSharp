using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Users;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }

    public string UserRoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
