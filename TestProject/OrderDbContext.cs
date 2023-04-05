using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderDbContext
    {
        public OrderContext GetOrderContext()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new OrderContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Orders.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Orders.Add(new Order()
                    {
                        OrderId = i,
                        CustomerId = 1,
                        TotalAmount = 100*i,
                        AppliedDiscount = 10*i
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }
    }
}
