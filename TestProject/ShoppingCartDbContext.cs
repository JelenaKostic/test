using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ShoppingCartDbContext
    {
        public ShoppingCartContext GetShoppingCartContext()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ShoppingCartContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.ShoppingCarts.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.ShoppingCarts.Add(new ShoppingCart()
                    {
                        ShoppingCartId = i,
                        CustomerId = 1,
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
