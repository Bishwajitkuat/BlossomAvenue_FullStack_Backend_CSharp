using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CartsService;

namespace BlossomAvenue.Service.Repositories.Carts
{
    public interface ICartRepository
    {
        public Task<Cart>? GetCart(Guid cartId);
        public Task<Cart>? CreateCartItem(CartItem cartItem);
        public Task<Cart>? DeleteCartItem(Guid cartId, Guid cartItemId);

        public Task<CartItem>? GetCartItemByCartAndVariationId(Guid cartId, Guid variationId);

        public Task<Variation>? GetVariationById(Guid variationId);
        public Task<Cart>? UpdateCartItem(CartItem cartItem);
        public Task<Cart>? EmptyCart(Guid cartId);
    }
}