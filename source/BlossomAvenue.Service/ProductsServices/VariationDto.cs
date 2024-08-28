using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public class CreateVariationDto
    {
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }

        public Variation ConvertToVariation(Guid productId)
        {
            return new Variation
            {
                VariationId = Guid.NewGuid(),
                VariationName = this.VariationName,
                Price = this.Price,
                Inventory = this.Inventory,
                ProductId = productId
            };
        }
    }

}