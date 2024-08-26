using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class IProduct
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}