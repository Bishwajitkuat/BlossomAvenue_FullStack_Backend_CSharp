using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastructure.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private BlossomAvenueDbContext _context;

        public OrderRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Cart cart, Order order)
        {

            var newOrder = (await _context.Orders.AddAsync(order)).Entity;
            if (newOrder != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                foreach (var cartItem in cart.CartItems)
                {
                    var variation = await _context.Variations.FirstOrDefaultAsync(v => v.VariationId == cartItem.VariationId);
                    if (variation != null)
                    {
                        variation.Inventory -= cartItem.Quantity;
                    }
                }
            }
            if (await _context.SaveChangesAsync() > 0) return newOrder;
            return null;
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _context.Orders
                        .Include(o => o.OrderItems)
                        .Include(o => o.AddressDetail)
                        .ThenInclude(o => o.City)
                        .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<bool> UpdateOrder(Guid orderId, string orderStatus)
        {
            var order = await _context.Orders
                                     .FindAsync(orderId);

            if (order == null)
            {
                throw new RecordNotFoundException("order");
            }

            //order.OrderStatus = orderStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}