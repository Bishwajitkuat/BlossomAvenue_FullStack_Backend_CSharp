using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public class Variation : IVariation
    {
        public Guid VariationId { get; set; }
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        // relationship with product table
        public Guid ProductId { get; set; }

    }
}