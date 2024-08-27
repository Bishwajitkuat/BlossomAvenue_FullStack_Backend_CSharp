using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Infrastrcture.Repositories.Users;
using BlossomAvenue.Service.UsersService;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Service.Repositories.Categories;
using BlossomAvenue.Infrastrcture.Repositories.Categories;
using BlossomAvenue.Service.CategoriesService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlossomAvenueDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention()
    );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
// DI category repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// DI category management service
builder.Services.AddScoped<ICategoryManagement, CategoryManagement>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
