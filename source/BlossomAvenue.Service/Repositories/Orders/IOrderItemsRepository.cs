using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.Repositories.Orders
{
    public interface IOrderItemsRepository
    {
        public Task<bool> CreateOrderItems(OrderItem cartItem);
        public Task<OrderItem> GetOrderItem(Guid cartItemId);
    }
}