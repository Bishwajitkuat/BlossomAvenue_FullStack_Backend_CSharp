using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class Category : ICategory
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid ParentId { get; set; }
    }
}