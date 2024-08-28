using System;

namespace BlossomAvenue.Core.Products
{
    public class Category : ICategory
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentId { get; set; }
    }
}