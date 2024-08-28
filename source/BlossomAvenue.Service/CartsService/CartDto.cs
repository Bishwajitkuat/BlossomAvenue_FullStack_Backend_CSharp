using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CartsService
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}