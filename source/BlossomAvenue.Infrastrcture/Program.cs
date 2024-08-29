using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Infrastrcture.Repositories.Users;
using BlossomAvenue.Service.UsersService;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.Repositories.Categories;
using BlossomAvenue.Infrastrcture.Repositories.Categories;
using BlossomAvenue.Service.CategoriesService;
using BlossomAvenue.Service.Repositories.Cities;
using BlossomAvenue.Infrastrcture.Repositories.Cities;
using BlossomAvenue.Service.Repositories.Carts;
using BlossomAvenue.Infrastrcture.Repositories.Carts;
using BlossomAvenue.Service.CartsService;
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Infrastrcture.Cryptography;
using BlossomAvenue.Service.Repositories.Orders;
using BlossomAvenue.Infrastrcture.Repositories.Orders;
using BlossomAvenue.Service.OrdersService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Infrastrcture.Repositories.Jwt;
using BlossomAvenue.Service.AuthenticationService;
using Microsoft.OpenApi.Models;
using BlossomAvenue.Service.Repositories.InMemory;
using BlossomAvenue.Presentation.Middleware;
using Microsoft.AspNetCore.Authorization;
using BlossomAvenue.Service.Repositories.Products;
using BlossomAvenue.Infrastrcture.Repositories.Products;
using BlossomAvenue.Service.ProductsServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlossomAvenueDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention()
    );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/** Domain DI Container */

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryManagement, CategoryManagement>();

// DI cart repository
builder.Services.AddScoped<ICartRepository, CartRepository>();
// DI cart management service
builder.Services.AddScoped<ICartManagement, CartManagement>();

// DI cart items repository
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
// DI cart items management service
builder.Services.AddScoped<ICartItemsManagement, CartItemsManagement>();

// DI Order repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// DI order management service
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
// DI product repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// DI product management service
builder.Services.AddScoped<IProductManagement, ProductManagement>();

builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthManagement, AuthManagement>();
builder.Services.AddSingleton<IInMemoryDB, InMemoryDB>();
builder.Services.AddTransient<IJwtManagement, JwtManagement>();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));

/** Domain DI Container End */

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
            ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrUserIdPolicy", policy =>
        policy.RequireAssertion(context => IsAdminOrMatchingUserId(context))
    );

    options.AddPolicy("UserIdPolicy", policy =>
        policy.RequireAssertion(context => IsMatchingUserId(context))
    );
});

bool IsAdminOrMatchingUserId(AuthorizationHandlerContext context)
{
    var roleClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

    var userIdClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

    var routeUserId = GetRouteUserId(context);

    return roleClaim == "Admin" || userIdClaim == routeUserId;
}
bool IsMatchingUserId(AuthorizationHandlerContext context)
{
    var userIdClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

    var routeUserId = GetRouteUserId(context);

    return userIdClaim is not null && routeUserId is not null && userIdClaim == routeUserId;
}
string GetRouteUserId(AuthorizationHandlerContext context)
{
    return context.Resource is HttpContext httpContext
        ? httpContext?.Request.RouteValues["userId"]?.ToString()
        : null;
}

builder.Services.AddControllers(options =>
{

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<TokenValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
