using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlossomAvenue.Core.Orders;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }
    public Guid AddressDetailId { get; set; }
    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    public DateTime? CreatedAt { get; set; }
    public virtual AddressDetail AddressDetail { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
