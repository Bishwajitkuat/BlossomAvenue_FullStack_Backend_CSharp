# Products endpoints

### URL

#### Authentication Required: `False`

```
base_url/api/v1/products?pageNo=1&PageSize=10&search=a&OrderWith=price&OrderBy=asc
```

### Response on success

- Get all products
- Pagination (page number and item per page)
- Search by product title
- Sort by price or min price or ratings
- Sort order ASC or DESC

```json
HTTP status 200 with

  [
  {
    "productId": "d942e61a-6ebc-42c3-8af3-f6f559ba3a3c",
    "title": "Red Roses",
    "description": "Red Roses description",
    "imageUrl": "img1",
    "minPrice": 10,
    "avgStar": 0,
    "inventory": 16
  },
  {
    "productId": "a623c8e6-1deb-4527-81d8-e119a6cbbcc3",
    "title": "Test2 update Roses",
    "description": "White Roses description",
    "imageUrl": "img1",
    "minPrice": 10,
    "avgStar": 0,
    "inventory": 38
  },
  {
    "productId": "935583f5-78a8-4fea-acc7-f3a080446776",
    "title": "Test3 Roses",
    "description": "White Roses description",
    "imageUrl": "img1",
    "minPrice": 15,
    "avgStar": 0,
    "inventory": 8
  },
  {
    "productId": "5811c1ac-1136-4753-9285-698d861a3ff2",
    "title": "Test Roses",
    "description": "White Roses description",
    "imageUrl": "img1",
    "minPrice": 15,
    "avgStar": 0,
    "inventory": 8
  },
  {
    "productId": "5c14efe0-ec0b-4298-9961-78802a69f9e1",
    "title": "White Roses",
    "description": "White Roses description",
    "imageUrl": "img1",
    "minPrice": 15,
    "avgStar": 0,
    "inventory": 8
  }
]

```

### Responses on fail

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

#### Authentication Required: `False`

```
GET base_url/api/v1/products/{id}
```

### Response on success

- Get a product by id(guid)

```json
HTTP status 200 with

  {
  "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "description": "string",
  "images": [
    {
      "imageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "imageUrl": "string",
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ],
  "variations": [
    {
      "variationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "variationName": "string",
      "price": 0,
      "inventory": 0,
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ],
  "categories": [
    {
      "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "categoryName": "string",
      "parentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ],
  "productReviews": [
    {
      "reviewId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "review": "string",
      "star": 0
    }
  ],
  "avgStar": 0
}
```

### Responses on fail

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

#### Authentication Required as Admin: `True`

```
POST base_url/api/v1/products
```

- Create a new product

### Request Body

```json
{
  "title": "White rose", // string
  "description": "product description", // test
  "variations": [
    {
      "variation_name": "150 white rose", // string
      "price": 66.5, // decimal
      "inventory": 12 // int
    },
    {
      "variation_name": "200 white rose", // string
      "price": 74.99, // decimal
      "inventory": 5 // int
    }
  ],
  "images": [
    {
      "image_url": "https://images.unsplash.com/photo-1509449764226-63afec1c342a?q=80&w=1976&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" // text
    },
    {
      "image_url": "https://images.unsplash.com/photo-1525258946800-98cfd641d0de?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" // text
    }
  ],
  "categories": [
    {
      "category_id": "1ff53c9b-4489-4258-8a32-0dea3710f205", // guid
      "category_name": "Rose", // string
      "parent_id": "798e5283-f46f-47c2-891f-6fdf80b367eb" // guid
    },
    {
      "category_id": "1f7593d5-2a5d-4138-a9d3-dcd47f8811bd", // guid
      "category_name": "Season",
      "parent_id": "d455fc4c-ceeb-478f-a830-0e38b4e08434" //null or guid
    }
  ]
}
```

### Response on success

```json
HTTP status 201 with

{
    "productId": "18416587-65d5-47c3-8d7f-6986fb2456c2",
    "title": "Test7 Roses",
    "description": "White Roses description",
    "images": [
        {
            "imageId": "12dfe949-767b-45fc-a1ad-6d2ef4c2827e",
            "imageUrl": "img1",
            "productId": "18416587-65d5-47c3-8d7f-6986fb2456c2"
        }
    ],
    "variations": [
        {
            "variationId": "c2cc5fc6-3dfe-47ac-931a-68ab47a32b7b",
            "variationName": "10 Test7 Roses",
            "price": 15,
            "inventory": 3,
            "productId": "18416587-65d5-47c3-8d7f-6986fb2456c2"
        },
        {
            "variationId": "e5e2044c-40d4-4475-970f-6fc81162337b",
            "variationName": "25 Test7 Roses",
            "price": 24.99,
            "inventory": 5,
            "productId": "18416587-65d5-47c3-8d7f-6986fb2456c2"
        }
    ],
    "productCategories": [
        {
            "productCategoryId": "4117b731-6489-43a2-a02c-b98b91e0539c",
            "categoryId": "ac24cb4d-d431-47b8-a106-8ffd1fe88c88",
            "productId": "18416587-65d5-47c3-8d7f-6986fb2456c2",
            "category": null
        }
    ],
    "productReviews": null
}
```

### Response on fail

```json
HTTP status 404 with
{
  "message": "Invalid category_id or parent_id"  // string
}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized." // string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

#### Authentication Required as Admin: `True`

```
PATCH base_url/api/v1/products/{id}
```

- Update a product by Id (guid)

### Request Body

```json
{
  "title": "Test Roses",
  "description": "White Roses description",
  "images": [
    {
      "imageId": "cbf028d7-03f0-49ca-8597-1faa527a542a",
      "imageUrl": "img1",
      "productId": "5811c1ac-1136-4753-9285-698d861a3ff2"
    }
  ],
  "variations": [
    {
      "variationId": "170084c9-b31f-448f-aed8-873f6d236a6b",
      "variationName": "25 Test Roses",
      "price": 19.99,
      "inventory": 5,
      "productId": "5811c1ac-1136-4753-9285-698d861a3ff2"
    },
    {
      "variationId": "4eceac89-19de-482b-891d-5a08b310e186",
      "variationName": "10 Test Roses",
      "price": 15,
      "inventory": 3,
      "productId": "5811c1ac-1136-4753-9285-698d861a3ff2"
    }
  ],
  "productcategories": [
    {
      "categoryId": "ac24cb4d-d431-47b8-a106-8ffd1fe88c88",
      "categoryName": "Rose",
      "parentId": "a9ca6749-b518-4db4-8f65-9bc07d7e938e"
    }
  ]
}
```

### Response on success

```json
HTTP status 204 with
{}
```

### Response on fail

```json
HTTP status 404 with
{
  "message": "Invalid product id or product object to update the product."  // string
}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized." // string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

#### Authentication Required as Admin: `True`

```
DELETE base_url/api/v1/products/{id}
```

- Delete a product by Id(guid)

### Response on success

```json
HTTP status 204 with
{}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized." // string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
