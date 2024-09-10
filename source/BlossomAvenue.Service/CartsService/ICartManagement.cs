using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public interface ICartManagement
    {
        public Task<Cart> GetCart(Guid cartId);
        public Task<Cart> AddItemToCart(CartItem cartItem);
        public Task<Cart> DeleteCartItem(Guid cartId, Guid cartItemId);
        public Task<Cart> ReduceItemQtyFromCartItem(CartItem cartItem);
        public Task<Cart> EmptyCart(Guid cartId);
    }
}