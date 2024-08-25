# Products endpoints

### URL

#### Authentication Required: `False`

```
base_url/api/v1/products?page=page_number&items_per_page=item_number&search_by=search_word&order_with=name&order_by=asc | desc
```

### Response on success

- Get all products
- Pagination (page number and item per page)
- Search by product title
- Sort by price

```json
HTTP status 200 with
{
  "product_count": 111, // int
  "page": 1, // int
  "items_per_page": 10 // int
  "products": [
  {
    "product_id" : "9722b5b6-27a3-4990-9b21-5aa71c47418d", // guid
    "title": "White rose", // string
    "description": "product description", // test
    "min_price": 26.56 // decimal
    "images": "https://images.unsplash.com/photo-1509449764226-63afec1c342a?q=80&w=1976&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // text
  }

]
}
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
    "product_id" : "9722b5b6-27a3-4990-9b21-5aa71c47418d", // guid
    "title": "White rose", // string
    "description": "product description", // test
    "variations":[
      {
        "variation_id": "798e5283-f46f-47c2-891f-6fdf80b367eb", // guid
        "variation_name": "50 white rose", // string
        "price": 26.50, // decimal
        "inventory": 12, // int
        "product_id" : "9722b5b6-27a3-4990-9b21-5aa71c47418d", // guid
      },
      {
        "variation_id": "ac833de7-1c42-49a6-850c-041be666991a", // guid
        "variation_name": "100 white rose", // string
        "price": 44.99, // decimal
        "inventory": 5, // int
        "product_id" : "9722b5b6-27a3-4990-9b21-5aa71c47418d", // guid
      }
    ],
    "images":[
      {
        "image_id": "1ff53c9b-4489-4258-8a32-0dea3710f205", // guid
        "image_url": "https://images.unsplash.com/photo-1509449764226-63afec1c342a?q=80&w=1976&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // text

        "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
      },
      {
        "image_id": "27e84fc1-402d-4621-83e5-23cc42d13abd", // guid
        "image_url": "https://images.unsplash.com/photo-1525258946800-98cfd641d0de?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // text

        "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
      },
    ],
    "categories": [
      {
        "category_id" : "1ff53c9b-4489-4258-8a32-0dea3710f205", // guid
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
HTTP status 204 with

{}
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
  "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d", // guid
  "title": "White rose special", // string
  "description": "product description", // test
  "variations": [
    {
      "variation_id": "798e5283-f46f-47c2-891f-6fdf80b367eb", // guid
      "variation_name": "50 white rose special", // string
      "price": 36.5, // decimal
      "inventory": 12, // int
      "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
    },
    {
      "variation_id": "ac833de7-1c42-49a6-850c-041be666991a", // guid
      "variation_name": "100 white rose special", // string
      "price": 54.99, // decimal
      "inventory": 5, // int
      "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
    }
  ],
  "images": [
    {
      "image_id": "1ff53c9b-4489-4258-8a32-0dea3710f205", // guid
      "image_url": "https://images.unsplash.com/photo-1509449764226-63afec1c342a?q=80&w=1976&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // text

      "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
    },
    {
      "image_id": "27e84fc1-402d-4621-83e5-23cc42d13abd", // guid
      "image_url": "https://images.unsplash.com/photo-1525258946800-98cfd641d0de?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // text

      "product_id": "9722b5b6-27a3-4990-9b21-5aa71c47418d" // guid
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
