using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.Repositories.Carts;
using BlossomAvenue.Infrastrcture.Database;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private BlossomAvenueDbContext _context;

        public CartRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }

        // public Task<bool> ClearCart(Guid cartId)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<bool> DeleteProductFromCart(Guid productId)
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<Cart> GetCart(Guid userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        // public Task<bool> UpdateCart(Guid cartId, Guid productId, int quantity)
        // {
        //     throw new NotImplementedException();
        // }
    }
}