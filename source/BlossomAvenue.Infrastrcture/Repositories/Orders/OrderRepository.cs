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
        public async Task<bool> CreateOrder(Guid cartId, Guid userId, decimal amount)
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
                TotalAmount = amount,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemsId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    VariationId = cartItem.Variationid,
                    Quantity = cartItem.Quantity
                };

                order.OrderItems.Add(orderItem);
            }

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