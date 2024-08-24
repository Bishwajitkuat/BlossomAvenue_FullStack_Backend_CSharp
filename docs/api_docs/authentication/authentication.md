# Authentication Endpoints

- [Login Endpoint](#login)

- [Logout Endpoint](#logout)

## Login

#### Authentication Required : `False`

### URL

```
POST base_url/api/v1/auth/login
```

### Request Body

```json
{
  "username": "username", // username | string
  "password": "password" // password | string
}
```

### Response on success

```json
{
  "accessToken": "access-token" // jwt access token | string
}
```

### Responses on fail

```json
HTTP status 401 with
{
    "message": "Username or Password error" // message | string
}
```

```json
HTTP status 400 with
{
    "message": "Username | Password cannot be empty" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Logout

#### Authentication Required : `True`

```
POST base_url/api/v1/auth/logout
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```
HTTP status 204 - No content
```

### Responses on failures

```json
HTTP status 401
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 500
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
