using System;
using System.Collections;
using System.Collections.Generic;

namespace BlossomAvenue.Core.Products
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}