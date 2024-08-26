using System;

namespace BlossomAvenue.Core.ProductAggregate
{
    public interface IVariation
    {
        public Guid VariationId { get; set; }
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public Guid ProductId { get; set; }
    }
}