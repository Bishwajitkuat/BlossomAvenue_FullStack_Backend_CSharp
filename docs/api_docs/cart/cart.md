# Cart Endpoints

- [Get cart details](#url)

- [Create order](#url-1)

- [Get all orders](#url-2)


## Get cart details

#### Authentication Required : `True`

### URL

```
GET base_url/api/v1/cart/
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
{
  "cart_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
  "user_id": "74dbab55-e414-4888-a04d-90a66bae2f15", // guid
  "cart_status": "completed", // type | varchar
  "created_at": "1724255258", // datetime
  "total_amount": 123.45, // numeric
  "cart_items": [
    {
        "cart_item_id": "c407e402-1b07-442c-bedb-4d335daf4e00", //guid 
        "cart_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
    },
    {
        "cart_item_id": "c407e402-1b07-442c-bedb-4d335daf4e00", //guid 
        "cart_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
    },
  ]
}
```

### Responses on fail

```json
HTTP status 401 with
{
    "message": "Invalid access token" // message | string
}
```

```json
HTTP status 404 with
{
    "message": "Cart not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Update cart

#### Authentication Required : `True`

### URL
```
PATCH base_url/api/v1/cart/user_id/:product_id?qty=product_qty
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
{
    "message" : "Cart updated successfully!" // message | string
}
```

### Responses on fail

```json
HTTP status 401 with
{
    "message": "Invalid access token" // message | string
}
```

```json
HTTP status 404 with
{
    "message": "Product not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Remove product from cart

#### Authentication Required : `True`

### URL

```
DELETE base_url/api/v1/cart/user_id/:product_id
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
```json
{
    "message" : "Prodcut removed from the cart successfully!" // message | string
}
```

### Responses on fail

```json
HTTP status 401 with
{
    "message": "Invalid access token" // message | string
}
```

```json
HTTP status 404 with
{
    "message": "Product not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
