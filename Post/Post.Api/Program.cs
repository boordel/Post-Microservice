using Microsoft.EntityFrameworkCore;
using Post.Infra;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context
builder.Services.AddDbContext<PostDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PostDB"),
       sqlOptions =>
       {
           sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
           sqlOptions.EnableRetryOnFailure(
               maxRetryCount: 10,
               maxRetryDelay: TimeSpan.FromSeconds(30),
               errorNumbersToAdd: null);
       })
);

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
