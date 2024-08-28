using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.CartsService;

namespace BlossomAvenue.Service.Repositories.Carts
{
    public interface ICartRepository
    {
        public Task <Cart> GetCart(Guid userId);
        // public Task<bool> CreateCart(Category category);
        // public Task<bool> UpdateCart(Guid cartId, Guid productId, int quantity);
        // public Task<bool> DeleteProductFromCart(Guid productId);
        // public Task<bool> ClearCart(Guid cartId);
    }
}