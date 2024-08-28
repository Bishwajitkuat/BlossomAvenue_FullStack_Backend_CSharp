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
    public class CartsController : ControllerBase
    {
        private ICartManagement _cartManagement;

        public CartsController(ICartManagement cartManagement)
        {
            _cartManagement = cartManagement;
        }

        [HttpGet("{userId}")]
        public async Task<Cart> GetCart(Guid userId)
        {
            return await _cartManagement.GetCart(userId);

        }


    }
}