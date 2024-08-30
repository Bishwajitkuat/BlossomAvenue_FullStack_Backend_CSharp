using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.ProductReviewsService
{
    public class ProductReviewCreateDto
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public string? Review { get; set; }

        public int? Star { get; set; }
    }
}