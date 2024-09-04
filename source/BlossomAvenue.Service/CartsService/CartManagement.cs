using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public class CartManagement : ICartManagement
    {
        private ICartRepository _cartRepository;
        public CartManagement(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartDto> GetCart(Guid cartId)
        {
            return await _cartRepository.GetCart(cartId);
        }
    }
}