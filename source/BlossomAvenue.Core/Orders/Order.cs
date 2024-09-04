using BlossomAvenue.Core.ProductReviews;
using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Orders;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public Guid? AddressId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string OrderStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
}
