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
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderManagement _orderManagement;
        public OrderController(IOrderManagement orderManagement)
        {
            _orderManagement = orderManagement;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            // should get from claim
            var cartId = new Guid("dde2c6dc-3c14-4ce8-bb02-c81f3e2a59c1");
            var userId = new Guid("fbfbcb7c-32f0-42d4-b5b8-862f8f7105ff");
            var order = await _orderManagement.CreateOrder(cartId, createOrderDto, userId);
            return Created(nameof(CreateOrder), order);
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

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _orderManagement.GetOrder(orderId);
            if (order == null)
            {
                throw new RecordNotFoundException("order");
            }

            return Ok(order);
        }

    }
}