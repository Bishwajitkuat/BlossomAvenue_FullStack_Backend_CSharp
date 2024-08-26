using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<IImage> Images { get; set; }
        public IEnumerable<IVariation> Variations { get; set; }
        public IEnumerable<IProductCategory> ProductCategories { get; set; }
    }
}