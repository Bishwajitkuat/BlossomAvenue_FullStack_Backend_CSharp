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
        public async Task<Cart> GetCart(Guid userId)
        {
            return await _cartRepository.GetCart(userId);
        }
        // public async Task<bool> ClearCart(Guid cartId)
        // {
        //     var result = await _cartRepository.ClearCart(cartId);
        //     if (result == false) throw new RecordNotFoundException("category");
        //     return result;
        // }

        // public async Task<bool> DeleteProductFromCart(Guid productId)
        // {
        //     var result = await _cartRepository.DeleteProductFromCart(productId);
        //     if(result == false) throw new RecordNotFoundException("cart");
        //     return result;
        // }

        // public async Task<bool> UpdateCart(Guid cartId, Guid productId, int quantity)
        // {
        //     var result = await _cartRepository.UpdateCart(cartId, productId, quantity); 
        //     if(result == false) throw new RecordNotFoundException("cart");
        //     return result;
        // }
    }
}