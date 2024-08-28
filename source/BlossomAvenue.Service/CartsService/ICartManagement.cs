using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public interface ICartManagement
    {
        public Task <Cart> GetCart(Guid userId);
        // public Task<bool> CreateCart(Category category);
        // public Task<bool> UpdateCart(Guid cartId, Guid productId, int quantity);
        // public Task<bool> DeleteProductFromCart(Guid productId);
        // public Task<bool> ClearCart(Guid cartId);
    }
}