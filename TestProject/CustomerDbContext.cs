using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CustomerDbContext
    {
        public CustomerContext GetCustomerContext()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CustomerContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Customers.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Customers.Add(new Customer()
                    {
                        CustomerId = i,
                       Address = new Address()
                       {
                           City = "Beograd",
                           Street = "Milutina Milankovica",
                           HouseNr = i.ToString()
                       },
                       PhoneNumber = "06077889"+(i.ToString())
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }
    }
}
