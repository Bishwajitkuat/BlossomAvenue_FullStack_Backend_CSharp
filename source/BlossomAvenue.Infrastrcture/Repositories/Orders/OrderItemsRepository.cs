using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.Repositories.Orders;

namespace BlossomAvenue.Infrastrcture.Repositories.Orders
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private BlossomAvenueDbContext _context;

        public OrderItemsRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }

        public Task<bool> CreateOrderItems(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetOrderItem(Guid cartItemId)
        {
            throw new NotImplementedException();
        }
    }
}