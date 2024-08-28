using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public class CreateProductDto

    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateImageDto> Images { get; set; }
        public IEnumerable<CreateVariationDto> Variations { get; set; }
        public IEnumerable<CreateProductCategoryDto> ProductCategories { get; set; }

        public Product ConvertToProduct()
        {
            Guid productId = Guid.NewGuid();
            IEnumerable<Image> images = Images.Select(i => i.ConvertToImage(productId));
            IEnumerable<Variation> variations = Variations.Select(v => v.ConvertToVariation(productId));
            IEnumerable<ProductCategory> productCategories = ProductCategories.Select(pc => pc.ConvertToProductCategory(productId));

            return new Product
            {
                ProductId = productId,
                Title = this.Title,
                Description = this.Description,
                Images = images,
                Variations = variations,
                ProductCategories = productCategories,
            };


        }
    }


    public class UpdateProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Variation> Variations { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}