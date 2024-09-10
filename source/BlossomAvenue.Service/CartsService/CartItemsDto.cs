using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public class CreateCartItemsDto
    {
        public Guid ProductId { get; set; }
        public Guid VariationId { get; set; }
        public int Quantity { get; set; }

        public CartItem ConvertToCartItems()
        {
            return new CartItem
            {
                ProductId = ProductId,
                Quantity = Quantity,
                VariationId = VariationId
            };
        }

    }

    public class CartItemDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        public Guid VariationId { get; set; }
        public int? Quantity { get; set; }
    }


    public class ReadCartItemDto
    {
        public Guid CartItemsId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VariationId { get; set; }
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ReadCartItemDto(CartItem cartItem)
        {
            CartItemsId = cartItem.CartItemsId;
            ProductId = cartItem.Variation.ProductId;
            VariationId = cartItem.Variation.VariationId;
            VariationName = cartItem.Variation.VariationName;
            Quantity = cartItem.Quantity;
            Price = cartItem.Variation.Price;

        }

    }

}