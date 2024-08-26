using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Variation> Variations { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}