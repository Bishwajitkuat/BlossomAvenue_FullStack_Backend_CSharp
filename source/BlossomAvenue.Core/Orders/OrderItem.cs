using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Core.Orders;

public partial class OrderItem
{
    [Key]
    public Guid OrderItemsId { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
