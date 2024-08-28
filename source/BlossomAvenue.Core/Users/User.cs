using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Users;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public Guid UserRoleId { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool? IsUserActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual UserRole UserRole { get; set; } = null!;

    public virtual ICollection<UserContactNumber> UserContactNumbers { get; set; } = new List<UserContactNumber>();

    public virtual UserCredential UserCredential { get; set; } = null!;
}
