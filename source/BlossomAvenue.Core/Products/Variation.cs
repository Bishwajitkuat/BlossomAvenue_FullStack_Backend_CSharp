using System;

namespace BlossomAvenue.Core.Products
{
    public class Variation
    {
        public Guid VariationId { get; set; }
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        // relationship with product table
        public Guid ProductId { get; set; }

    }
}