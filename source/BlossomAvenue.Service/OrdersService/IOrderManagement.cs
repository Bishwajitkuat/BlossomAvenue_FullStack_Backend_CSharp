using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public interface IOrderManagement
    {
        public Task<bool> CreateOrder(Guid cartId, Guid userId);
        public Task <Order> GetCart(Guid cartId);

        public Task <bool> UpdateOrder(Guid orderId, string orderStatus); 
        
    }
}