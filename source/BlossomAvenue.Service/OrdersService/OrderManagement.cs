using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Carts;
using BlossomAvenue.Service.Repositories.Orders;

namespace BlossomAvenue.Service.OrdersService
{
    public class OrderManagement : IOrderManagement
    {
        private IOrderRepository _orderRepository;
        private ICartRepository _cartRepository;

        public OrderManagement(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public async Task<Order> CreateOrder(Guid cartId, CreateOrderDto createOrderDto, Guid userId)
        {

            // get user cart
            var cart = await _cartRepository.GetCart(cartId) ?? throw new RecordNotFoundException("cart");
            // check variation inventory against purchase qty
            foreach (var cartItem in cart.CartItems)
            {
                var variation = await _cartRepository.GetVariationById(cartItem.VariationId) ?? throw new RecordNotFoundException("product");
                if (variation.Inventory - cartItem.Quantity < 0) throw new ProductOutOfStockException(variation.VariationName, variation.Inventory);
            }
            // if everything is fine use OrderCreateDto to return new order object, pass cart into it
            // pass new order and cart to new order and remove items from cart to CreateOrder method in orderRepo.
            // should return created order.
            Order order = createOrderDto.ConvertToOrder(cart, userId);
            var newOrder = await _orderRepository.CreateOrder(cart, order);
            if (newOrder == null) throw new RecordNotCreatedException("order");
            return newOrder;
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrder(orderId) ?? throw new RecordNotFoundException("order");
            return order;
        }

        public async Task<bool> UpdateOrder(Guid orderId, string orderStatus)
        {
            return await _orderRepository.UpdateOrder(orderId, orderStatus);
        }
    }
}