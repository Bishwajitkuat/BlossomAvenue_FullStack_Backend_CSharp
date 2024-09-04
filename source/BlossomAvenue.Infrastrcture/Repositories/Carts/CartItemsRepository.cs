using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Carts;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Carts
{
    public class CartItemsRepository : ICartItemsRepository
    {

        private BlossomAvenueDbContext _context;

        public CartItemsRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProductFromCart(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return false;
            }

            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateCartItems(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateCart(Guid cartItemId, Guid productId, Guid variantId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                throw new RecordNotFoundException("cartItem");
            }

            cartItem.ProductId = productId;
            cartItem.Variationid = variantId;
            cartItem.Quantity = quantity;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CartItem> GetCartItem(Guid cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }
    }
}