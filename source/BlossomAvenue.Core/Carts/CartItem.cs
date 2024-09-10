using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Core.Carts;

public partial class CartItem
{
    public Guid CartItemsId { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public Guid VariationId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Variation Variation { get; set; }
}
