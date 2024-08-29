using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.OrdersService;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderManagement _orderManagement;
        public OrderController(IOrderManagement orderManagement)
        {
            _orderManagement = orderManagement;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateOrder(Guid cartId, Guid userId)
        {
            try
            {
                var success = await _orderManagement.CreateOrder(cartId, userId);

                if (success)
                {
                    return Ok(new { Message = "Order created successfully." });
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