using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public interface IOrderItemsManagement
    {
        public Task<bool> CreateOrderItems(OrderItem cartItem);
        public Task<OrderItem> GetOrderItem(Guid cartItemId);
    }
}