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
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<CreateUpdateProductCategoryDto> ProductCategories { get; set; }

        public Product ConvertToProduct()
        {

            return new Product
            {
                Title = Title,
                Description = Description,
                Images = Images,
                Variations = Variations,
                ProductCategories = ProductCategories.Select(pc => pc.ConvertToProductCategory()).ToList(),
            };


        }
    }


    public class UpdateProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<CreateUpdateProductCategoryDto> ProductCategories { get; set; }

        public Product UpdateProduct(Product product)
        {
            product.Title = Title;
            product.Description = Description;
            product.Images = Images;
            product.Variations = Variations;
            product.ProductCategories = ProductCategories.Select(pc => pc.ConvertToProductCategory()).ToList();
            return product;
        }
    }

    public class GetAllProductReadDto
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal MinPrice { get; set; }
        public int Inventory { get; set; }
        public decimal AvgStar { get; set; }

        public GetAllProductReadDto(Product product)
        {
            ProductId = product.ProductId;
            Title = product.Title;
            Description = product.Description;
            ImageUrl = product.Images.FirstOrDefault().ImageUrl;
            MinPrice = product.Variations.Select(v => v.Price).Min();
            Inventory = product.Variations.Select(v => v.Inventory).Sum();

            var stars = product.ProductReviews.Where(r => r.Star != null).Select(r => r.Star);
            if (stars.Count() > 0)
            {
                AvgStar = (decimal)stars.Average();
            }
        }
    }



    public class GetProductByIdReadDto
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Variation> Variations { get; set; }
        public ICollection<ReadProductCategoryDto> ProductCategories { get; set; }
        public ICollection<ReadProductReviewDto> ProductReviews { get; set; }
        public decimal AvgStar { get; set; }

        public GetProductByIdReadDto(Product product)
        {
            ProductId = product.ProductId;
            Title = product.Title;
            Description = product.Description;
            Images = product.Images;
            Variations = product.Variations;
            ProductCategories = product.ProductCategories.Select(pc => new ReadProductCategoryDto(pc)).ToList();
            ProductReviews = product.ProductReviews.Select(pr => new ReadProductReviewDto(pr)).ToList();
            var stars = product.ProductReviews.Where(pr => pr.Star != null).Select(pr => pr.Star);
            if (stars.Count() > 0)
            {
                AvgStar = (decimal)stars.Average();
            }
        }

    }









}