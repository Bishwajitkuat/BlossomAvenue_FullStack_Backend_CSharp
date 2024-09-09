using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public class ReadProductCategoryDto
    {
        public Guid ProductCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
        public string CategoryName { get; set; }

        public ReadProductCategoryDto(ProductCategory pc)
        {
            ProductCategoryId = pc.ProductCategoryId;
            CategoryId = pc.CategoryId;
            ProductId = pc.ProductId;
            CategoryName = pc.Category.CategoryName;
        }
    }


    public class CreateUpdateProductCategoryDto
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