using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.OrdersService
{
    public class OrderQueryDto : SharedPagination
    {
        public OrderOrderWith? OrderWith { get; set; } = OrderOrderWith.CreatedAt;

        public OrderStatus? OrderStatus { get; set; }
    }
}