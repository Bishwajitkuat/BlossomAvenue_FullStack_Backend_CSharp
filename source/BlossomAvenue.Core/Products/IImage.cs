using System;

namespace BlossomAvenue.Core.Products
{
    public class IImage
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
    }
}