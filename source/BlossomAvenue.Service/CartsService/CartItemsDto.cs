using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public class CreateCartItemsDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VariationId { get; set; }
        public int Quantity { get; set; }

        public CartItem ConvertToCartitems()
        {
            return new CartItem { CartItemsId = Guid.NewGuid(), CartId = this.CartId, ProductId = this.ProductId, Quantity = this.Quantity, VariationId = this.VariationId };
        }

    }

    public class CartItemDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        public Guid VariationId { get; set; }
        public int? Quantity { get; set; }
    }

}