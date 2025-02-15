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

    public Guid VariationId { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
    public virtual Variation Variation { get; set; }
}
