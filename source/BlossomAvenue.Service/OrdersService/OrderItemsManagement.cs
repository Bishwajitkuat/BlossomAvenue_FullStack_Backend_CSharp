using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public class OrderItemsManagement : IOrderItemsManagement
    {
        public Task<bool> CreateOrderItems(OrderItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetOrderItem(Guid cartItemId)
        {
            throw new NotImplementedException();
        }
    }
}