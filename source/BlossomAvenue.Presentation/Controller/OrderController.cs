using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.OrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class OrderController : ControllerBase
    {
        private IOrderManagement _orderManagement;
        public OrderController(IOrderManagement orderManagement)
        {
            _orderManagement = orderManagement;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReadOrderDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            // should get from claim
            var cartId = GetCartIdFromClaim();
            var userId = GetUserIdFromClaim();
            var order = await _orderManagement.CreateOrder(cartId, createOrderDto, userId);
            var readOrder = new ReadOrderDto(order);
            return Created(nameof(CreateOrder), readOrder);
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> GetOrderByIdByUser([FromRoute] Guid orderId)
        {
            var userId = GetUserIdFromClaim();
            var order = await _orderManagement.GetOrder(orderId, userId);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ICollection<ReadOrderDto>>> GetAllOrdersByUser([FromQuery] OrderQueryDto oqdto)
        {
            var userId = GetUserIdFromClaim();
            var orders = await _orderManagement.GetAllOrdersByUser(oqdto, userId);
            var readOrders = orders.Select(o => new ReadOrderDto(o)).ToList();
            return Ok(readOrders);
        }


        // ADMIN OPERATIONS

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("admin")]
        public async Task<ActionResult<List<ReadOrderDto>>> GetAllOrdersByAdmin([FromQuery] OrderQueryDto oqdto)
        {
            var orders = await _orderManagement.GetAllOrdersByAdmin(oqdto);
            var readOrders = orders.Select(o => new ReadOrderDto(o)).ToList();
            return Ok(readOrders);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("admin/{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> GetOrderByIdByAdmin([FromRoute] Guid orderId)
        {
            var order = await _orderManagement.GetOrder(orderId, null);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPatch("admin/{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> UpdateOrder([FromRoute] Guid orderId, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderManagement.UpdateOrder(orderId, orderUpdateDto);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("admin/{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdByAdmin([FromRoute] Guid orderId)
        {
            var status = await _orderManagement.DeleteOrderById(orderId);
            if (status) return NoContent();
            else throw new RecordNotFoundException("order");
        }


        // helper function to get cart id from claim
        private Guid GetCartIdFromClaim()
        {
            var claims = HttpContext.User;
            var cartId = claims.FindFirst("CartId") ?? throw new UnauthorizedAccessException();
            return new Guid(cartId.Value);
        }

        // helper function to get user id from claim
        private Guid GetUserIdFromClaim()
        {
            var claims = HttpContext.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
            return new Guid(userId.Value);
        }

    }
}