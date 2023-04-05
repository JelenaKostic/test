using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductDbContext
    {
        public ProductContext GetProductContext()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ProductContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Products.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Products.Add(new Product()
                    {
                        ProductId = i,
                        ProductName = $"Product {i}",
                        AvailableQuantity = i * 10,
                        Price = 10
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }
    }
}
