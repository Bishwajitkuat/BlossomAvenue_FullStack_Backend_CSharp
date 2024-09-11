using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Service.OrdersService;

namespace BlossomAvenue.Service.Repositories.Orders
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Cart cart, Order order);
        public Task<Order>? GetOrder(Guid orderId);
        public Task<Order> UpdateOrder(Order order);
        public Task<ICollection<Order>> GetAllOrdersByUser(OrderQueryDto oqdto, Guid userId);
        public Task<ICollection<Order>> GetAllOrdersByAdmin(OrderQueryDto oqdto);
        public Task<bool> DeleteOrderById(Guid orderId);
    }
}