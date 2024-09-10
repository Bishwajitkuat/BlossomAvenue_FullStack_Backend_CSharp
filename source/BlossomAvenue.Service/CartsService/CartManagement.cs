using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Products;
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
        public async Task<Cart> GetCart(Guid cartId)
        {
            var cart = await _cartRepository.GetCart(cartId);
            if (cart == null) throw new RecordNotFoundException("cart");
            return cart;
        }

        public async Task<Cart> AddItemToCart(CartItem cartItem)
        {
            // fetch the cartItem by variationId && cartId, if exist then increate the qty by one.
            var oldCartItem = await _cartRepository.GetCartItemByCartAndVariationId(cartItem.CartId, cartItem.VariationId);
            // fetch the variation to check stock
            Variation? variation = await _cartRepository.GetVariationById(cartItem.VariationId) ?? throw new ArgumentException("The product can not be found to be added in cart");
            // if oldCartItem is null and qty more than stock
            if (oldCartItem == null)
            {
                if (variation.Inventory - cartItem.Quantity < 0) throw new ProductOutOfStockException(variation.VariationName, variation.Inventory);
                var cart = await _cartRepository.CreateCartItem(cartItem) ?? throw new RecordNotCreatedException("cart items");
                return cart;
            }
            else
            {

                oldCartItem.Quantity += 1;
                if (variation.Inventory - oldCartItem.Quantity < 0) throw new ProductOutOfStockException(variation.VariationName, variation.Inventory);
                var cart = await _cartRepository.UpdateCartItem(oldCartItem) ?? throw new RecordNotFoundException("cart item");
                return cart;
            }
        }

        public async Task<Cart> ReduceItemQtyFromCartItem(CartItem cartItem)
        {
            var oldCartItem = await _cartRepository.GetCartItemByCartAndVariationId(cartItem.CartId, cartItem.VariationId);
            if (oldCartItem == null) throw new RecordNotFoundException("Cart item");
            oldCartItem.Quantity -= 1;
            if (oldCartItem.Quantity < 1)
            {
                return await DeleteCartItem(oldCartItem.CartId, oldCartItem.CartItemsId);
            }
            else
            {
                var cart = await _cartRepository.UpdateCartItem(oldCartItem);
                if (cart == null) throw new RecordNotFoundException("cart item");
                return cart;
            }
        }

        public async Task<Cart> DeleteCartItem(Guid cartId, Guid cartItemId)
        {
            var cart = await _cartRepository.DeleteCartItem(cartId, cartItemId);
            if (cart == null) throw new ArgumentException("Sorry! Could not found the item in your cart");
            return cart;
        }

        public async Task<Cart> EmptyCart(Guid cartId)
        {
            var cart = await _cartRepository.EmptyCart(cartId) ?? throw new RecordNotFoundException("cart");
            return cart;
        }



    }
}