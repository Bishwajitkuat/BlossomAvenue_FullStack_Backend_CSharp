using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.Repositories.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public interface ICartItemsManagement
    {
        public Task<bool> CreateCartItems(CartItem cartItem);
        public Task<bool> UpdateCart(Guid cartItemId, Guid productId, Guid variantId, int quantity);
        public Task<bool> DeleteProductFromCart(Guid cartitemId);
        public Task <CartItem> GetCartItem(Guid cartItemId);
        // public Task<bool> ClearCart(Guid cartId);
    }
}