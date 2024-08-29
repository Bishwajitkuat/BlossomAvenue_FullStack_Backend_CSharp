using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Service.CartsService;

namespace BlossomAvenue.Service.Repositories.Carts
{
    public interface ICartRepository
    {
        public Task <CartDto> GetCart(Guid cartId);
    }
}