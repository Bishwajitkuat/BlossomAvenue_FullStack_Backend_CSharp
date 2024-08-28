using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{

    public class CreateImageDto
    {
        public string ImageUrl { get; set; }

        public Image ConvertToImage()
        {
            return new Image { ImageId = Guid.NewGuid(), ImageUrl = this.ImageUrl };
        }
    }










}