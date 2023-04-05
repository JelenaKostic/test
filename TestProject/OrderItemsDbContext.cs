using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderItemDbContext
    {
        public OrderItemContext GetOrderItemContext()
        {
            var options = new DbContextOptionsBuilder<OrderItemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new OrderItemContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.OrderItems.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.OrderItems.Add(new OrderItem()
                    {
                        OrderItemId = i,
                        OrderId = i,
                        ProductId = 1,
                        Quantity = 10
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }
    }
}
