using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Infrastructure.Repositories.Users;
using BlossomAvenue.Service.UsersService;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.Repositories.Categories;
using BlossomAvenue.Infrastructure.Repositories.Categories;
using BlossomAvenue.Service.CategoriesService;
using BlossomAvenue.Service.Repositories.Carts;
using BlossomAvenue.Infrastructure.Repositories.Carts;
using BlossomAvenue.Service.CartsService;
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Infrastructure.Cryptography;
using BlossomAvenue.Service.Repositories.Orders;
using BlossomAvenue.Infrastructure.Repositories.Orders;
using BlossomAvenue.Service.OrdersService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Service.AuthenticationService;
using Microsoft.OpenApi.Models;
using BlossomAvenue.Presentation.Middleware;
using BlossomAvenue.Service.Repositories.ProductReviews;
using BlossomAvenue.Infrastructure.Repositories.ProductReviews;
using BlossomAvenue.Service.ProductReviewsService;
using Microsoft.AspNetCore.Authorization;
using BlossomAvenue.Service.Repositories.Products;
using BlossomAvenue.Infrastructure.Repositories.Products;
using BlossomAvenue.Service.ProductsServices;
using System.Text.Json.Serialization;
using BlossomAvenue.Infrastructure.Token.Jwt;
using BlossomAvenue.Service.Repositories.Authentications;
using BlossomAvenue.Infrastructure.Repositories.RefreshTokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlossomAvenueDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention()
    );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Cookies

builder.Services.Configure<CookiePolicyOptions>(option =>
{
    option.OnAppendCookie = context =>
        {
            if (context.CookieOptions.Secure && context.CookieOptions.SameSite == SameSiteMode.None)
            {
                context.CookieOptions.Extensions.Add("Partitioned");
            }
        };
});


// CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("https://blossomavenue.vercel.app", "http://localhost:3000", "http://localhost:5212")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials().SetIsOriginAllowedToAllowWildcardSubdomains();

                      });
});

/** Domain DI Container */
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryManagement, CategoryManagement>();

// DI cart repository
builder.Services.AddScoped<ICartRepository, CartRepository>();
// DI cart management service
builder.Services.AddScoped<ICartManagement, CartManagement>();

// DI Order repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// DI order management service
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
// DI product repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// DI product management service
builder.Services.AddScoped<IProductManagement, ProductManagement>();

// DI Product review repository
builder.Services.AddScoped<IProductReviewRepository, ProductReviewsRepository>();
// DI order management service
builder.Services.AddScoped<IProductReviewManagement, ProductReviewManagement>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthManagement, AuthManagement>();
builder.Services.AddTransient<ITokenManagement, TokenManagement>();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));
// DI Exception middleware
builder.Services.AddScoped<ExceptionMiddleware>();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:Secret"])),
        };
    });

builder.Services.AddControllers(options =>
{

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = false;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseDeveloperExceptionPage();

app.UseCookiePolicy();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
