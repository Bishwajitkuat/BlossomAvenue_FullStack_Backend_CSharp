using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.UsersService.Dtos;

namespace BlossomAvenue.Service.OrdersService
{
    public class ReadOrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public virtual ReadAddressDetailDto ShippingAddress { get; set; }
        public virtual ICollection<ReadOrderItemDto> OrderItems { get; set; }

        public ReadOrderDto(Order order)
        {
            OrderId = order.OrderId;
            CreatedAt = order.CreatedAt;
            TotalAmount = order.TotalAmount;
            OrderStatus = order.OrderStatus;
            ShippingAddress = new ReadAddressDetailDto(order.AddressDetail);
            OrderItems = order.OrderItems.Select(oi => new ReadOrderItemDto(oi)).ToList();
        }

    }

    public class ReadOrderItemDto
    {
        public Guid OrderItemsId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid VariationId { get; set; }
        public string VariationName { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public ReadOrderItemDto(OrderItem oi)
        {
            OrderItemsId = oi.OrderItemsId;
            Quantity = oi.Quantity;
            Price = oi.Price;
            VariationId = oi.VariationId;
            VariationName = oi.Variation.VariationName;
            ProductId = oi.ProductId;
            Title = oi.Product.Title;
            ImageUrl = oi.Product.Images.ToList()[0].ImageUrl;
        }


    }


}