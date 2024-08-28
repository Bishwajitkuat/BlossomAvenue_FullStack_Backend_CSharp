using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.CartsService;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private ICartItemsManagement _cartItemsManagement;

        public CartItemsController(ICartItemsManagement cartItemsManagement)
        {
            _cartItemsManagement = cartItemsManagement;
        }

        [HttpPost("")]
        public async Task<bool> CreateCartItems(CreateCartItemsDto cartItemsDto)
        {
            var cartItem = cartItemsDto.ConvertToCartitems();
            return await _cartItemsManagement.CreateCartItems(cartItem);

        }

        [HttpGet("{cartItemId}")]
        public async Task<IActionResult> GetCartItem(Guid cartItemId)
        {
            var item = await _cartItemsManagement.GetCartItem(cartItemId);
            return Ok(item);
        }

        [HttpPatch("")]
        public async Task<IActionResult> UpdateCart([FromQuery] Guid cartItemId, [FromQuery] Guid productId, [FromQuery] int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity can not be less than or equal 0");

            var result = await _cartItemsManagement.UpdateCart(cartItemId, productId, quantity);

            if (result)
            {
                return Ok(new { Message = "Cart item updated successfully." });
            }
            else
            {
                return NotFound(new { Message = "Cart item not found." });
            }

        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteProductFromCart(Guid cartItemId)
        {
            var result = await _cartItemsManagement.DeleteProductFromCart(cartItemId);
            if (result)
            {
                return Ok(new { Message = "Product removed from cart successfully." });
            }
            else
            {
                return NotFound(new { Message = "Product not found in cart." });
            }
        }
    }
}