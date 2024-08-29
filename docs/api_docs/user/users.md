# Users Endpoints

- [Create User](#create-user)

- [Create Profile](#create-profile)

- [Get Users](#get-users)

- [Get Profile By Id](#get-profile-by-id)

- [Update User](#update-user)

- [Active Inactive User](#active-inactive-user)

- [Delete Profile](#delete-profile)

## Create User

#### Authentication Required : `True`

#### User Role : `Admin`

### URL

```
POST base_url/api/v1/users
```

### Request Body

```json
{
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email" // string
}
```

### Response on success

```json
HTTP status 201 with
{
  "userId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email" // string
}
```

### Responses on fail

```json
HTTP status 400 with
{
    "message": "first name | last name | email cannot be empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Create Profile

#### Authentication Required : `False`

### URL

```
POST base_url/api/v1/profile
```

### Request Body

```json
{
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email", // string
  "password": "password", //string
  "contactNumber": ["1122334455", "2211335566"], // string array
  "addressLine1": "address line 1", // string
  "addressLine2": "address line 2", // string
  "cityId": "cba8be27-52f5-42a0-b2b9-055aac01f293" // guid
}
```

### Password Rules

```
- Required
- At least 8 characters
- Should contain at least one digit
- Should contain at least one uppercase letter
- Should contain at least one lowercase letter
- Should contain at least one special character
```

### Response on success

```json
HTTP status 201 with
{
  "userId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email", // string
  "contactNumber": ["1122334455", "2211335566"], // string array
  "addressLine1": "address line 1", // string
  "addressLine2": "address line 2", // string
  "cityId": "cba8be27-52f5-42a0-b2b9-055aac01f293" // guid
}
```

### Responses on fail

```json
HTTP status 400 with
{
    "message": "first name | last name | email | contact number | address | city cannot be empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Get Users

#### Authentication Required : `True`

#### User Role : `Admin`

### URL

```
GET base_url/api/v1/users?
    userRoleId=cba8be27-52f5-42a0-b2b9-055aac01f293&
    search=searchText&
    pageNo=1&
    pageSize=10&
    orderWith=firstName&
    orderBy=ASC
```

```
    userRoleId : Can be null,
    search : searchText
    pageNo : Cannot be 0. Always should be more than 0,
    pageSize: Cannot be 0. Always should be more than 0
    orderWith: Possible values `firstName` | `lastName` | `roleName`
    orderBy: Possible values `ASC` | `DESC`

```

### Response on success

```json
HTTP status 201 with
{
    [
        {
            "userId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
            "firstName": "firstName", // string
            "lastName": "lastName", // string
            "email": "email", // string
            "userRoleId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
            "userRoleName": "userRoleName", // Admin | Customer  string
            "isUserActive": true // true | false bool
        }
    ]
}
```

### Responses on fail

```json
HTTP status 400 with
{
    "message": "first name | last name | email cannot be empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Get Profile By Id

#### Authentication Required : `True`

### User Role : `Owner User | Admin`

### URL

```
GET base_url/api/v1/profile/{userId}
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Response on success

```json
HTTP status 200 with
{
  "userId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email", // string
  "contactNumber": ["1122334455", "2211335566"],
  "addresses": [
    {
      "addressId": "cba8be27-52f5-42a0-b2b9-055aac01f293", // guid
      "isDefaultAddress": "TRUE", // TRUE | FALSE
      "addressLine1": "addressLine1", // string
      "addressLine2": "addressLine2", //string
      "cityId": "cba8be27-52f5-42a0-b2b9-055aac01f293" // guid
    }
  ]
}
```

### Responses on fail

```json
HTTP status 400 with
{
    "message": "user id cannot be empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Update user

#### Authentication Required : `True`

### Authenticated User : `Owner only`

### URL

```
PATCH base_url/api/v1/user/{userId}
```

### Request Header

```
Request Headers:
Authorization: Bearer <access-token>
```

### Request Body

```json
{
  "firstName": "firstName", // string
  "lastName": "lastName", // string
  "email": "email" // string
}
```

### Response on success

```
HTTP status 204 - No content
```

### Responses on fail

```json
HTTP status 400 with
{
    "message": "first name | last name | email cannot be empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```

## Active Inactive User

#### Authentication Required : `True`

#### User Role : `Admin`

### URL

```
PATCH base_url/api/v1/activeuser?userId={userId}&status={status}
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

### Responses on fail

```json
HTTP status 400 with
{
    "message": "isActive cannot be null or empty" // message | string
}
```

```json
HTTP status 401 with
{
    "message": "Unauthorized" // message | string
}
```

```json
HTTP status 403 with
{
    "message": "Forbidden" // message | string
}
```

```json
HTTP status 500 with
{
    "message": "Something unusual happened. Please wait and try again or contact system administrator" // message | string
}
```
