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
    [Route("api/v1/[controller]s")]
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
            // ensure only user can add items to their own cart
            var cartId = new Guid("56f20816-ffc6-41a2-83a3-a399e1844a47");
            var cart = await _cartMg.GetCart(cartId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<ReadCartDto>> AddItemToCart(CreateCartItemsDto cartItemsDto)
        {
            var cartItem = cartItemsDto.ConvertToCartItems();
            // from claim
            // ensure only user can add items to their own cart
            var cartId = new Guid("56f20816-ffc6-41a2-83a3-a399e1844a47");
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
            // ensure only user can add items to their own cart
            var cartId = new Guid("56f20816-ffc6-41a2-83a3-a399e1844a47");
            cartItem.CartId = cartId;
            var cart = await _cartMg.ReduceItemQtyFromCartItem(cartItem);
            var readCart = new ReadCartDto(cart);
            return Created(nameof(AddItemToCart), readCart);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult<ReadCartDto>> DeleteCartItem([FromRoute] Guid cartItemId)
        {
            var cartId = new Guid("56f20816-ffc6-41a2-83a3-a399e1844a47");
            var cart = await _cartMg.DeleteCartItem(cartId, cartItemId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }

        [HttpDelete]
        public async Task<ActionResult<ReadCartDto>> EmptyCart()
        {
            var cartId = new Guid("56f20816-ffc6-41a2-83a3-a399e1844a47");
            var cart = await _cartMg.EmptyCart(cartId);
            var readCart = new ReadCartDto(cart);
            return Ok(readCart);
        }






    }
}