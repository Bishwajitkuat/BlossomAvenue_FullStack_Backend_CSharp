using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Infrastructure.Database.SeedData
{
    public static class SeedDataProducts
    {
        public static List<Product> Products { get; } = [
            new Product{
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365"),
                Title = "Read Rose",
                Description = "Description"
            },
                new Product{
                ProductId = new Guid("20b5078f-aa53-4d02-a337-f00f7564cd7b"),
                Title = "White Rose Basket",
                Description = "Description"
            },
                new Product{
                ProductId = new Guid("03b932b6-9016-4300-84d9-705bad989a26"),
                Title = "Rose and Chrysanthemum Bouquet",
                Description = "Description"
            },
                new Product{
                ProductId = new Guid("94c86365-cf5f-4807-902b-21121c8724a4"),
                Title = "Rainbow Roses",
                Description = "Description"
            }
        ];

        public static List<Variation> Variations { get; } = [
            new Variation{
                VariationId = new Guid("6968824e-c49d-42d7-bed6-d2a1e29dbc78"),
                VariationName = "150 Red Roses",
                Price = 1100,
                Inventory = 5,
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365")
            },
            new Variation{
                VariationId = new Guid("77a1154d-96a4-4877-8a66-79c9ce9dc08e"),
                VariationName = "15 Red Roses",
                Price = 74.99m,
                Inventory = 10,
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365")
            },
            new Variation{
                VariationId = new Guid("8ddc691c-34c0-4c8e-867b-96a3df4c936c"),
                VariationName = "White Rose Basket Small",
                Price = 49.99m,
                Inventory = 12,
                ProductId = new Guid("20b5078f-aa53-4d02-a337-f00f7564cd7b")
            },
            new Variation{
                VariationId = new Guid("3b7355e8-6eed-4c1d-9639-ae0fe0d56294"),
                VariationName = "White Rose Basket Special",
                Price = 199.99m,
                Inventory = 5,
                ProductId = new Guid("20b5078f-aa53-4d02-a337-f00f7564cd7b")
            },
            new Variation{
                VariationId = new Guid("22558dcb-ed61-4a8d-869d-4a36c5dc3f14"),
                VariationName = "Rose and Chrysanthemum Bouquet Regular",
                Price = 89,
                Inventory = 9,
                ProductId = new Guid("03b932b6-9016-4300-84d9-705bad989a26")
            },
            new Variation{
                VariationId = new Guid("af8242b7-15dc-4a22-99d9-70b191947daa"),
                VariationName = "Rainbow Roses Small",
                Price = 55,
                Inventory = 25,
                ProductId = new Guid("94c86365-cf5f-4807-902b-21121c8724a4")
            },
        ];

        public static List<Image> Images { get; } = [
            new Image{
                ImageId = new Guid("341b12a1-7cdb-4db6-854c-f97a7f17851a"),
                ImageUrl = "https://images.unsplash.com/photo-1512056495345-913a0c261dc8?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHJlZCUyMHJvc2UlMjBib3VxdWV0fGVufDB8fDB8fHww",
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365")
            },
            new Image{
                ImageId = new Guid("8f774a3a-440f-4fca-b6fb-0d83cc241f6c"),
                ImageUrl = "https://images.unsplash.com/photo-1646928112435-fe66d4d2a465?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTh8fHdoaXRlJTIwcm9zZSUyMGJvdXF1ZXR8ZW58MHx8MHx8fDA%3D",
                ProductId = new Guid("20b5078f-aa53-4d02-a337-f00f7564cd7b")
            },
            new Image{
                ImageId = new Guid("cc6520b4-4060-4fff-8161-a101b447ed96"),
                ImageUrl = "https://images.unsplash.com/photo-1646928112435-fe66d4d2a465?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8Um9zZSUyMGFuZCUyMENocnlzYW50aGVtdW0lMjBCb3VxdWV0fGVufDB8fDB8fHww",
                ProductId = new Guid("03b932b6-9016-4300-84d9-705bad989a26")
            },
            new Image
            {
                ImageId = new Guid("1ebe2acc-a63a-4bc6-a7dc-6b745d3d307f"),
                ImageUrl = "https://plus.unsplash.com/premium_photo-1677004108526-b0897a63a21d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Um9zZXMlMjBib3VxdWV0fGVufDB8fDB8fHww",
                ProductId = new Guid("94c86365-cf5f-4807-902b-21121c8724a4")
            }
        ];

        public static List<ProductCategory> ProductCategories { get; } = [
            new ProductCategory{
                ProductCategoryId = new Guid("62d31328-eedf-485c-a6aa-f28f9dc8560e"),
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365"),
                CategoryId = new Guid("c8b87d25-8504-40d7-b60b-8423428716b5")
            },
            new ProductCategory{
                ProductCategoryId = new Guid("81a4357b-4d68-4284-be76-cefb8113ca40"),
                ProductId = new Guid("a33e7a83-7678-4acf-93d3-48577e6fd365"),
                CategoryId = new Guid("a2921ff1-6274-44d9-9148-3e8207a1e14c")
            },
            new ProductCategory{
                ProductCategoryId = new Guid("cde16aa0-3218-4c67-bdc2-d46bce1980d7"),
                ProductId = new Guid("20b5078f-aa53-4d02-a337-f00f7564cd7b"),
                CategoryId = new Guid("c8b87d25-8504-40d7-b60b-8423428716b5")
            },
            new ProductCategory{
                ProductCategoryId = new Guid("eef8c33d-c072-4ee6-8a58-5b136f787a66"),
                ProductId = new Guid("03b932b6-9016-4300-84d9-705bad989a26"),
                CategoryId = new Guid("2a50dc2f-395f-43bd-82d2-08438c4e7c4b")
            },
            new ProductCategory{
                ProductCategoryId = new Guid("5ba618dd-d3dd-4a43-9bac-ec98f527e72e"),
                ProductId = new Guid("94c86365-cf5f-4807-902b-21121c8724a4"),
                CategoryId = new Guid("20d8a69d-297d-4410-b895-8e0767ac1942")
            }
        ];




    }
}