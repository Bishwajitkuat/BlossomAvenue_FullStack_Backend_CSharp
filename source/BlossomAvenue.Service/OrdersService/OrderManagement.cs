using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Service.Repositories.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public class OrderManagement : IOrderManagement
    {
        private IOrderRepository _orderRepository;

        public OrderManagement (IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> CreateOrder(Guid cartId, Guid userId)
        {
            return await _orderRepository.CreateOrder(cartId, userId);
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _orderRepository.GetOrder(orderId);
        }

        public async Task<bool> UpdateOrder(Guid orderId, string orderStatus)
        {
            return await _orderRepository.UpdateOrder(orderId, orderStatus);
        }
    }
}