using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Core.ProductReviews;

public partial class ProductReview
{
    [Key]
    public Guid ReviewId { get; set; }

    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }

    public string? Review { get; set; }

    public int? Star { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
