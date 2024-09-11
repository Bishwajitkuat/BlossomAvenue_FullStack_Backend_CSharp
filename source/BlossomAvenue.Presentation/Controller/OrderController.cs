using System;
using System.Collections.Generic;
using System.Linq;
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


        //[Authorize(Roles = "Admin, Employee")]
        [HttpPatch("{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> UpdateOrder([FromRoute] Guid orderId, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderManagement.UpdateOrder(orderId, orderUpdateDto);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> GetOrderByIdByUser([FromRoute] Guid orderId)
        {
            var userId = Guid.NewGuid();
            var order = await _orderManagement.GetOrder(orderId, userId);
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

        [HttpGet("admin/{orderId}")]
        public async Task<ActionResult<ReadOrderDto>> GetOrderByIdByAdmin([FromRoute] Guid orderId)
        {
            var order = await _orderManagement.GetOrder(orderId, null);
            var readOrder = new ReadOrderDto(order);
            return Ok(readOrder);
        }

    }
}