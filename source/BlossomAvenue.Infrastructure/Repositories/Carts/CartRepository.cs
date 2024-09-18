using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.Repositories.Carts;
using BlossomAvenue.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.CartsService;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Infrastructure.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private BlossomAvenueDbContext _context;

        public CartRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<Cart>? GetCart(Guid cartId)
        {
            var cart = await _context.Carts
                        .Include(c => c.CartItems)
                            .ThenInclude(ci => ci.Product)
                                .ThenInclude(p => p.Images.Take(1))
                        .Include(c => c.CartItems)
                            .ThenInclude(ci => ci.Variation)
                        .FirstOrDefaultAsync(c => c.CartId == cartId);
            return cart;
        }

        public async Task<Cart>? CreateCartItem(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            if (await _context.SaveChangesAsync() > 0) return await GetCart(cartItem.CartId);
            else return null;

        }

        public async Task<Cart>? UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            if (await _context.SaveChangesAsync() > 0) return await GetCart(cartItem.CartId);
            else return null;

        }


        public async Task<Cart>? DeleteCartItem(Guid cartId, Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.CartItemsId == cartItemId);
            if (cartItem == null) return null;
            _context.Remove(cartItem);
            if (await _context.SaveChangesAsync() > 0) return await GetCart(cartId);

            return null;
        }


        public async Task<CartItem>? GetCartItemByCartAndVariationId(Guid cartId, Guid variationId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.Variation.VariationId == variationId);
            return cartItem;
        }

        // should be in product repo
        // deal with lather after completing order
        public async Task<Variation>? GetVariationById(Guid variationId)
        {
            var variation = await _context.Variations.FirstOrDefaultAsync(v => v.VariationId == variationId);
            return variation;
        }


        public async Task<Cart>? EmptyCart(Guid cartId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == cartId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return await GetCart(cartId);
        }














    }
}