using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.OrdersService;
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


        [HttpPost]
        public async Task<ActionResult<ReadOrderDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            // should get from claim
            var cartId = new Guid("dde2c6dc-3c14-4ce8-bb02-c81f3e2a59c1");
            var userId = new Guid("fbfbcb7c-32f0-42d4-b5b8-862f8f7105ff");
            var order = await _orderManagement.CreateOrder(cartId, createOrderDto, userId);
            var readOrder = new ReadOrderDto(order);
            return Created(nameof(CreateOrder), readOrder);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrder(Guid orderId, string orderStatus)
        {
            try
            {
                var success = await _orderManagement.UpdateOrder(orderId, orderStatus);

                if (success)
                {
                    return Ok(new { Message = "Order updated successfully." });
                }
                else
                {
                    throw new Exception("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> GetOrder([FromRoute] Guid orderId)
        {
            var order = await _orderManagement.GetOrder(orderId);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ReadOrderDto>>> GetAllOrdersByUser([FromQuery] OrderQueryDto oqdto)
        {
            var userId = new Guid("fbfbcb7c-32f0-42d4-b5b8-862f8f7105ff");
            var orders = await _orderManagement.GetAllOrdersByUser(oqdto, userId);
            var readOrders = orders.Select(o => new ReadOrderDto(o)).ToList();
            return Ok(readOrders);
        }

        [HttpGet("admin")]
        public async Task<ActionResult<List<ReadOrderDto>>> GetAllOrdersByAdmin([FromQuery] OrderQueryDto oqdto)
        {
            var orders = await _orderManagement.GetAllOrdersByAdmin(oqdto);
            var readOrders = orders.Select(o => new ReadOrderDto(o)).ToList();
            return Ok(readOrders);
        }

    }
}