using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Users;

namespace BlossomAvenue.Service.OrdersService
{
    public class CreateOrderDto
    {
        public Guid? AddressId { get; set; }
        public CreateShippingAddressDto ShippingAddress { get; set; }

        public Order ConvertToOrder(Cart cart, Guid userId)
        {
            var order = new Order
            {
                UserId = userId,
            };
            if (AddressId != null)
            {
                order.AddressId = (Guid)AddressId;
            }
            else if (ShippingAddress != null)
            {
                order.AddressDetail = ShippingAddress.ConvertToAddressDetail();
            }

            decimal totalPrice = 0;
            List<OrderItem> orderItems = [];
            foreach (var cartItem in cart.CartItems)
            {
                totalPrice += cartItem.Quantity * cartItem.Variation.Price;
                orderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    VariationId = cartItem.VariationId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Variation.Price
                });
            }

            order.OrderItems = orderItems;
            order.TotalAmount = totalPrice;

            return order;
        }
    }
}