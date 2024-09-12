using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Infrastructure.Database.SeedData
{
    public static class SeedDataCategories
    {
        public static List<Category> Categories { get; } = [
            new Category
            {
                CategoryId = new Guid("4951ec12-e459-4fb3-8869-93e7d9d3f6cf"),
                CategoryName = "Bouquets"
            },
            new Category
            {
                CategoryId = new Guid("29fecc10-b7da-4956-adfa-b964aa8be16a"),
                CategoryName = "Plants"
            },
            new Category
            {
                CategoryId = new Guid("d631b3fe-3c02-452a-8328-affcb6cab0cd"),
                CategoryName = "Flowers",
                ParentId = new Guid("4951ec12-e459-4fb3-8869-93e7d9d3f6cf")
            },
            new Category
            {
                CategoryId = new Guid("c8b87d25-8504-40d7-b60b-8423428716b5"),
                CategoryName = "Rose",
                ParentId = new Guid("d631b3fe-3c02-452a-8328-affcb6cab0cd")
            },
            new Category
            {
                CategoryId = new Guid("b2418f3b-8813-4803-bc2d-b87e9424144d"),
                CategoryName = "Lilies",
                ParentId = new Guid("d631b3fe-3c02-452a-8328-affcb6cab0cd")
            },
            new Category
            {
                CategoryId = new Guid("53c127bb-598d-43d7-bd5d-9fddd6015276"),
                CategoryName = "Season",
                ParentId = new Guid("4951ec12-e459-4fb3-8869-93e7d9d3f6cf")
            },
            new Category
            {
                CategoryId = new Guid("2a50dc2f-395f-43bd-82d2-08438c4e7c4b"),
                CategoryName = "Summer Bouquets",
                ParentId = new Guid("53c127bb-598d-43d7-bd5d-9fddd6015276")
            },
            new Category
            {
                CategoryId = new Guid("061ec68c-5ab4-435f-8913-f3a5aa870888"),
                CategoryName = "Spring Bouquets",
                ParentId = new Guid("53c127bb-598d-43d7-bd5d-9fddd6015276")
            },
            new Category
            {
                CategoryId = new Guid("3bdd3ae4-b386-42e7-ae90-bc5755588d3a"),
                CategoryName = "Occasions",
                ParentId = new Guid("4951ec12-e459-4fb3-8869-93e7d9d3f6cf")
            },
            new Category
            {
                CategoryId = new Guid("20d8a69d-297d-4410-b895-8e0767ac1942"),
                CategoryName = "Wedding",
                ParentId = new Guid("3bdd3ae4-b386-42e7-ae90-bc5755588d3a")
            },
            new Category
            {
                CategoryId = new Guid("a2921ff1-6274-44d9-9148-3e8207a1e14c"),
                CategoryName = "Love and Romance",
                ParentId = new Guid("3bdd3ae4-b386-42e7-ae90-bc5755588d3a")
            }
        ];

    }
}