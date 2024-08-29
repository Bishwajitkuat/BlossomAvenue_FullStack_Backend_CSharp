using System;

namespace BlossomAvenue.Core.Products
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        // relation with products table
        public Guid ProductId { get; set; }

    }
}