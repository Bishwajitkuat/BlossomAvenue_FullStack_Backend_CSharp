using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Core.Users;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public UserRole UserRole { get; set; } = UserRole.Customer;

    public DateTime? LastLogin { get; set; }

    public bool? IsUserActive { get; set; }

    public DateTime? CreatedAt { get; set; }
    public Cart Cart { get; set; } = new Cart();
    public virtual UserCredential UserCredential { get; set; } = null!;
    public virtual ICollection<UserContactNumber> UserContactNumbers { get; set; } = new List<UserContactNumber>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();



}
