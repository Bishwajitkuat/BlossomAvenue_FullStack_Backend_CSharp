using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Core.ProductReviews;

public partial class ProductReview
{
    public Guid ReviewId { get; set; }

    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public string Review { get; set; }

    public int Star { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }

}
