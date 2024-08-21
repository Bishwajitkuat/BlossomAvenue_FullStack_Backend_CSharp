# Order Endpoints

- [Get single order](#url1)

- [Create order](#url2)

- [Get all orders](#url3)


## Get single order

#### Authentication Required : `True`

### URL

```
GET base_url/api/v1/order/:order_id
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
{
  "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
  "user_id": "74dbab55-e414-4888-a04d-90a66bae2f15", // guid
  "status": "completed", // type | string
  "total_amount": 123.45, // numeric
  "address_id": "b1f37661-03e7-48da-a663-63bbf48e28de", // guid
  "created_at": "1724255258", // datetime
  "order_items": [
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
    },
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
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
    "message": "Order not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Create order

#### Authentication Required : `True`

### URL
```
POST base_url/api/v1/order
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```
### Request Body

```json
{
  "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
  "user_id": "74dbab55-e414-4888-a04d-90a66bae2f15", // guid
  "status": "pending", // type | string
  "total_amount": 123.45, // numeric
  "address_id": "b1f37661-03e7-48da-a663-63bbf48e28de", // guid
  "created_at": "1724255258", // datetime
  "order_items": [
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
    },
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
    },
  ]
}
```

### Response on success

```json
{
    "message" : "Order placed successfully!" // message | string
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
    "message": "Order not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Get all orders

#### Authentication Required : `True`

### URL

```
GET base_url/api/v1/order/
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
[{
  "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
  "user_id": "74dbab55-e414-4888-a04d-90a66bae2f15", // guid
  "status": "pending", // type | string
  "total_amount": 123.45, // numeric
  "address_id": "b1f37661-03e7-48da-a663-63bbf48e28de", // guid
  "created_at": "1724255258", // datetime
  "order_items": [
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
    },
    {
        "order_id": "b076da2b-7106-406c-9d7e-72737d5ef226", // guid
        "product_id": "5270aa63-9829-40cf-a404-da49b5ae7232", // guid
        "qualtity": 3, // int
        "price": 78.90 // numeric
    },
  ]
}]
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
    "message": "Order not found" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
