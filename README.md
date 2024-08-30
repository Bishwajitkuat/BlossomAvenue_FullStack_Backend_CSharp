# Blossom Avenue

## Overview

This project is the backend service for Blossom Avenue which is a flower shop application, developed using C# and the .NET Core framework. The backend manages various functionalities including user management, product management, and order processing. The system supports two types of users: **Admin** and **Customer**.

### Key Features

- **User Management**:
  - Admins can manage products, categories, and product variations.
  - Customers can browse products, place orders, and leave reviews.

- **Product & Category Management**:
  - Admins can create, update, delete, and view categories and products.
  - Supports product variations such as different sizes, colors, etc.

- **Product Reviews**:
  - Customers can create, update, delete, and view reviews for products.

### Technologies Used

- **.NET Core**: A cross-platform framework for building the backend API.
- **Entity Framework Core**: ORM for database interactions.
- **XUnit**: Unit testing framework for ensuring the reliability of the application.

## Project Structure

The project follows a clean architecture pattern, making it easy to maintain and scale.

- **Presentation**: Handle incoming HTTP requests and route them to the appropriate service.
- **Services**: Contain business logic for managing users, products, and orders.
- **Infrastructure**: Interact with the database using Entity Framework Core.
- **Core**: Define the data structure for users, products, orders, and reviews.
- **Tests**: Unit tests written using XUnit to ensure code quality and functionality.

## API Documentation

Detailed API documentation is available, which includes all the endpoints, request/response formats, and examples. You can access the API documentation [here](/docs/api_docs/).

## Setup and Installation

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any compatible database)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Steps to Run the Project

1. **Install Dependencies**:
   ```bash
   dotnet restore

2. **Set up database**:
    Update the connection string in appsettings.Development.json to match your database configuration.
    Run the following command to apply migrations
   ```bash
    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
            "DefaultConnection": "Host=you_host; Port=your_port;Database=db_name;Username=user_name;Password=db_pass"
        },
        "UserRoles": {
            "Admin": "Admin",
            "User": "Customer"
        },
        "JwtConfiguration": {
            "Secret": "myuR@HG95y*NQq^Jute5DHLBleb2EFsW",
            "Issuer": "http://localhost:5000",
            "Audience": "http://localhost:5000",
            "ExpiryMinutes": 15
        }
    }
    
3. **Run application**:
   ```bash
   cd source/BlossomAvenue.Ingrastructure
   dotnet run

4. **Run test**:
   ```bash
   dotnet test