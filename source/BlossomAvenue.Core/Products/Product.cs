using System;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Products
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}