using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.ProductsServices
{
    public class ProductQueryDto : SharedPagination
    {
        public ProductOrderWith? ProductOrderWith { get; set; }

        public Guid? CategoryId { get; set; }

    }
}