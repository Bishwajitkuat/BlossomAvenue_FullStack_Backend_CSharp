using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public class CreateProductCategoryDto
    {
        public Guid CategoryId { get; set; }

        public ProductCategory ConvertToProductCategory()
        {
            return new ProductCategory
            {
                ProductCategoryId = Guid.NewGuid(),
                CategoryId = this.CategoryId,
            };
        }

    }
}