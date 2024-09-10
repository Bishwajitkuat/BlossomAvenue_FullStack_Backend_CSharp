using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.Repositories.Orders
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Cart cart, Order order);
        public Task<Order>? GetOrder(Guid orderId);
        public Task<bool> UpdateOrder(Guid orderId, string orderStatus);
    }
}