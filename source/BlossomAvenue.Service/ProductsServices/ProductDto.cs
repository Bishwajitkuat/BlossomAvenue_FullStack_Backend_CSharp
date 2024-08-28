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
        public ICollection<CreateImageDto> Images { get; set; }
        public ICollection<CreateVariationDto> Variations { get; set; }
        public ICollection<CreateProductCategoryDto> ProductCategories { get; set; }

        public Product ConvertToProduct()
        {
            ICollection<Image> images = Images.Select(i => i.ConvertToImage()).ToArray();
            ICollection<Variation> variations = Variations.Select(v => v.ConvertToVariation()).ToArray();
            ICollection<ProductCategory> productCategories = ProductCategories.Select(pc => pc.ConvertToProductCategory()).ToArray();

            return new Product
            {
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
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

    public class GetAllProductReadDto
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal MinPrice { get; set; }
        public decimal AvgStar { get; set; }

    }



    public class GetProductByIdReadDto
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        // implement later
        // public decimal AvgStar { get; set; }

    }









}