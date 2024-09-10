﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Core.Orders;

public partial class OrderItem
{
    public Guid OrderItemsId { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid VariationId { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
    public Variation Variation { get; set; }
}
