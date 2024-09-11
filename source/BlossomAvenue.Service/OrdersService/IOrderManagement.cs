using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public interface IOrderManagement
    {
        public Task<Order> CreateOrder(Guid cartId, CreateOrderDto createOrderDto, Guid userId);
        public Task<Order> GetOrder(Guid orderId);

        public Task<bool> UpdateOrder(Guid orderId, string orderStatus);
        public Task<ICollection<Order>> GetAllOrdersByUser(OrderQueryDto oqdto, Guid userId);
        public Task<ICollection<Order>> GetAllOrdersByAdmin(OrderQueryDto oqdto);

    }
}