using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private BlossomAvenueDbContext _context;

        public OrderRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateOrder(Guid cartId, Guid userId)
        {
            var cart = await _context.Carts
                                     .Include(c => c.CartItems)
                                     .FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new InvalidOperationException("Cart is empty or does not exist.");
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                AddressId = null,
                CreatedAt = DateTime.UtcNow
            };

            decimal? totalAmount = 0;

            foreach (var cartItem in cart.CartItems)
            {
                var variation = await _context.Variations
                                      .FirstOrDefaultAsync(v => v.VariationId == cartItem.Variationid);

                if (variation == null)
                {
                    throw new InvalidOperationException("Invalid variation for a cart item.");
                }

                // Calculate the total price
                decimal itemPrice = variation.Price; // Assuming Variation has a Price property
                decimal? totalPrice = itemPrice * cartItem.Quantity;
                
                totalAmount += totalPrice;

                var orderItem = new OrderItem
                {
                    OrderItemsId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    VariationId = cartItem.Variationid,
                    Quantity = cartItem.Quantity,
                    Price = totalPrice
                };

                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Add(order);

            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            return true;
        }

        public Task<Order> GetCart(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}