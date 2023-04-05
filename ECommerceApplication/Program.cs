using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("Customer"));
builder.Services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase("Order"));
builder.Services.AddDbContext<OrderItemContext>(opt => opt.UseInMemoryDatabase("OrderItem"));
builder.Services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("Product"));
builder.Services.AddDbContext<ShoppingCartContext>(opt => opt.UseInMemoryDatabase("ShoppingCart"));

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
