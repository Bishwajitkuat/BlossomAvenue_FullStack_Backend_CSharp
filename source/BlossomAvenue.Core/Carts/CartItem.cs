﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Core.Carts;

public partial class CartItem
{
    [Key]
    public Guid CartItemsId { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;
}
