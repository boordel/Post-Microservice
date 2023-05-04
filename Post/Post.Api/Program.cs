using Microsoft.EntityFrameworkCore;
using Post.Api.Application.Repositories;
using Post.Domain.Entities.CategoryAggregate;
using Post.Infra;
using Post.Infra.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context
builder.Services.AddDbContext<PostDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"),
       sqlOptions =>
       {
           sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
           sqlOptions.EnableRetryOnFailure(
               maxRetryCount: 10,
               maxRetryDelay: TimeSpan.FromSeconds(30),
               errorNumbersToAdd: null);
       })
);

// Add memory cache
builder.Services.AddMemoryCache();

// Add and configure redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Add Repositories, just for test. Later we will change the algurithm
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryRepositoryMemoryCache>();
builder.Services.AddScoped<CategoryRepositoryRedisCache>();

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
