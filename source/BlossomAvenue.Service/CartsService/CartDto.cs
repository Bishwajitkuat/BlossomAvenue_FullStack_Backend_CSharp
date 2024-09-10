using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;

namespace BlossomAvenue.Service.CartsService
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class ReadCartDto
    {
        public Guid CartId { get; set; }

        public virtual ICollection<ReadCartItemDto> CartItems { get; set; }

        public ReadCartDto(Cart cart)
        {
            CartId = cart.CartId;
            CartItems = cart.CartItems.Select(ci => new ReadCartItemDto(ci)).ToList();
        }


    }




}