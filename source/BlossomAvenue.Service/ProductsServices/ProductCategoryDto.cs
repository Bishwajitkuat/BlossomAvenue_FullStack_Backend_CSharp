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
                CategoryId = this.CategoryId,
            };
        }

    }


    public class UpdateProductCategoryDto
    {
        public Guid ProductCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }

        public ProductCategory ConvertToProductCategory()
        {
            return new ProductCategory
            {
                ProductCategoryId = this.ProductCategoryId,
                CategoryId = this.CategoryId,
                ProductId = this.ProductId

            };
        }

    }
}