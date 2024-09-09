using System;
using System.Collections.Generic;
using BlossomAvenue.Core.ProductReviews;

namespace BlossomAvenue.Core.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Variation> Variations { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<ProductReview> ProductReviews { get; set; }
    }
}