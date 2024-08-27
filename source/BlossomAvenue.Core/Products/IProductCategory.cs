using System;

namespace BlossomAvenue.Core.Products
{
    public interface IProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}