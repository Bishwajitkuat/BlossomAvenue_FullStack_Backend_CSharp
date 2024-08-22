# Product categories endpoints

### URL

#### Authentication Required: `False`

```
GET base_url/api/v1/categories
```

### Response on success

```json
HTTP status 200 with
[
  {
    "category_id": "9fbac7dc-612f-4853-8957-16281e122941", // guid
    "category_name": "Bouquets",
    "parent_id": null // null or guid
  },
  {
    "category_id": "1f7593d5-2a5d-4138-a9d3-dcd47f8811bd", // guid
    "category_name": "Season",
    "parent_id": "d455fc4c-ceeb-478f-a830-0e38b4e08434" //null or guid
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

#### Authentication Required as Admin: `True`

```
POST base_url/api/v1/categories
```

### Request Body

```json
{
  "category_name": "Rose", // string
  "parent_id": "d455fc4c-ceeb-478f-a830-0e38b4e08434" //null or guid
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
  "message": "Category name is null or invalid parent id."  // string
}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized."
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
PATCH base_url/api/v1/categories/:id
```

### Request Body

```json
{
  "category_name": "Red Rose", // string
  "parent_id": "d455fc4c-ceeb-478f-a830-0e38b4e08434" //null or guid
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
  "message": "Category name is null or invalid category id or parent id."  // string
}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized."
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
DELETE base_url/api/v1/categories/:id
```

### Response on success

```json
HTTP status 204 with
{}
```

```json
HTTP status 401 with
{
  "message": "Unauthorized."
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
