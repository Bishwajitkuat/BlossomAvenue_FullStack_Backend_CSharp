using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.Repositories.Carts
{
    public interface ICartItemsRepository
    {
        public Task<bool> CreateCartItems(CartItem cartItem);
        public Task<bool> UpdateCart(Guid cartItemId, Guid productId, int quantity);
        public Task<bool> DeleteProductFromCart(Guid cartItemId);
        public Task <CartItem> GetCartItem(Guid cartItemId);
    }
}