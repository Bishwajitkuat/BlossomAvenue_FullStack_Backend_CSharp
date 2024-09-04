# Order Endpoints for Admin

- [Update order](#url)

- [Delete order](#url-1)

## Update order

#### Authentication Required : `True` (as admin)

### URL

```
PATCH base_url/api/v1/admin/order/:order_id
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Request body

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
HTTP status 403 with
{
    "message": "You don't have the permission to do it" // message | string
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

#### Authentication Required : `True` (as admin)

### URL
```
DELETE base_url/api/v1/admin/order/:order_id
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
{
    "message" : "Order deleted successfully!" // message | string
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
HTTP status 403 with
{
    "message": "You don't have the permission to do that." // message | string
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