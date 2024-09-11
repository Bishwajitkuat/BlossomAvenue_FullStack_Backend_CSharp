using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.OrdersService
{
    public class OrderUpdateDto
    {
        public OrderStatus OrderStatus { get; set; }
        public CreateShippingAddressDto? ShippingAddress { get; set; }

        public Order UpdateOrder(Order order)
        {
            order.OrderStatus = OrderStatus;
            if (ShippingAddress != null)
            {
                order.AddressDetail = ShippingAddress.ConvertToAddressDetail();
            }
            return order;
        }


    }
}