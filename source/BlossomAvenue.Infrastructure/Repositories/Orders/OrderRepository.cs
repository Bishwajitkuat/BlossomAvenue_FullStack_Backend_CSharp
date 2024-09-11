using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.OrdersService;
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
            if (await _context.SaveChangesAsync() > 0)
            {
                // return entity does not include the 2nd level relationship data
                // so fetching it again.
                var orderWithRelationData = await GetOrder(newOrder.OrderId);
                return orderWithRelationData;
            }
            return null;
        }

        public async Task<Order>? GetOrder(Guid orderId)
        {
            var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Variation)
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.Images)
            .Include(o => o.AddressDetail)
                .ThenInclude(o => o.City)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
            return order;
        }

        private async Task<ICollection<Order>> GetAllOrders(OrderQueryDto oqdto, Guid? userId)
        {
            var query = _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Variation)
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.Images)
            .Include(o => o.AddressDetail)
                .ThenInclude(o => o.City)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.UserId == userId);
            }

            // filter by status
            if (oqdto.OrderStatus.HasValue)
            {
                query = query.Where(u => u.OrderStatus == oqdto.OrderStatus);
            }
            // sorting
            var isAscending = oqdto.OrderBy.ToString().ToLower().Equals("asc", StringComparison.OrdinalIgnoreCase);

            query = oqdto.OrderWith switch
            {
                OrderOrderWith.TotalAmount => isAscending ? query.OrderBy(o => o.TotalAmount) : query.OrderByDescending(o => o.TotalAmount),
                OrderOrderWith.CreatedAt => isAscending ? query.OrderBy(o => o.CreatedAt) : query.OrderByDescending(o => o.CreatedAt),
                _ => isAscending ? query.OrderByDescending(o => o.CreatedAt) : query.OrderBy(o => o.CreatedAt)
            };

            var orders = await query
            .Skip((oqdto.PageNo - 1) * oqdto.PageSize)
            .Take(oqdto.PageSize)
            .ToListAsync();
            return orders;
        }

        public async Task<ICollection<Order>> GetAllOrdersByAdmin(OrderQueryDto oqdto)
        {
            return await GetAllOrders(oqdto, null);
        }
        public async Task<ICollection<Order>> GetAllOrdersByUser(OrderQueryDto oqdto, Guid userId)
        {
            return await GetAllOrders(oqdto, userId);
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