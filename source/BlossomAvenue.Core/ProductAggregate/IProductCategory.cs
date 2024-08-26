using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public interface IProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}