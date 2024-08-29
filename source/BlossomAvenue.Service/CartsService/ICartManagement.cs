using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public interface ICartManagement
    {
        public Task <CartDto> GetCart(Guid cartId);
    }
}