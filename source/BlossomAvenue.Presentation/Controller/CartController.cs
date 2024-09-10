using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.CartsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private ICartManagement _cartMg;

        public CartController(ICartManagement cartManagement)
        {
            _cartMg = cartManagement;
        }

        [HttpGet]
        public async Task<ActionResult<ReadCartDto>> GetCart()
        {
            // from claim
            // ensure only auth user get only his/her own cart
            var cartId = GetCartIdFromClaim();
            var cart = await _cartMg.GetCart(cartId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }

        [HttpPost]
        public async Task<ActionResult<ReadCartDto>> AddItemToCart(CreateCartItemsDto cartItemsDto)
        {
            var cartItem = cartItemsDto.ConvertToCartItems();
            // from claim
            // ensure only auth user can add items to their own cart
            var cartId = GetCartIdFromClaim();
            cartItem.CartId = cartId;
            var cart = await _cartMg.AddItemToCart(cartItem);
            var readCart = new ReadCartDto(cart);
            return Created(nameof(AddItemToCart), readCart);
        }

        [HttpPatch]
        public async Task<ActionResult<ReadCartDto>> ReduceItemQtyFromCartItem(CreateCartItemsDto cartItemsDto)
        {
            var cartItem = cartItemsDto.ConvertToCartItems();
            // from claim
            // ensure only auth user can reduce item quantity from their own cart
            var cartId = GetCartIdFromClaim();
            cartItem.CartId = cartId;
            var cart = await _cartMg.ReduceItemQtyFromCartItem(cartItem);
            var readCart = new ReadCartDto(cart);
            return Created(nameof(AddItemToCart), readCart);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult<ReadCartDto>> DeleteCartItem([FromRoute] Guid cartItemId)
        {
            var cartId = GetCartIdFromClaim();
            var cart = await _cartMg.DeleteCartItem(cartId, cartItemId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }

        [HttpDelete]
        public async Task<ActionResult<ReadCartDto>> EmptyCart()
        {
            var cartId = GetCartIdFromClaim();
            var cart = await _cartMg.EmptyCart(cartId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }

        // helper function to get cart id from claim
        private Guid GetCartIdFromClaim()
        {
            var claims = HttpContext.User;
            var cartId = claims.FindFirst("CartId") ?? throw new UnauthorizedAccessException();
            return new Guid(cartId.Value);
        }

    }
}