using System;


namespace BlossomAvenue.Core.ProductAggregate
{
    public class ProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        // reference to categories table
        public Guid CategoryId { get; set; }
        // reference to product table
        public Guid ProductId { get; set; }


    }
}