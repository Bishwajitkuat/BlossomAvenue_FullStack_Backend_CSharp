using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class IImage
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
    }
}