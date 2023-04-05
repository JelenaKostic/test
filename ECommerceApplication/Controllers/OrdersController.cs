using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApplication.Models;
using Castle.Core.Resource;
using NuGet.Versioning;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;
        private readonly OrderItemContext _orderItemContext;
        private readonly ShoppingCartContext _shoppingCartContext;
        private readonly ProductContext _productContext;
        private readonly CustomerContext _customerContext;

        public OrdersController(OrderContext context, OrderItemContext orderItemContext, ShoppingCartContext shoppingCartContext, ProductContext productContext, CustomerContext customerContext)
        {
            _context = context;
            _orderItemContext = orderItemContext;
            _shoppingCartContext = shoppingCartContext;
            _productContext = productContext;
            _customerContext = customerContext;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                {
                    return NotFound("This Order does not exist");
                }

                return order;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(long customerId, string phoneNumber, [FromBody]Address address)
        {
            try
            {
                var shoppingCarts = await _shoppingCartContext.ShoppingCarts.Where(sc => sc.CustomerId == customerId).ToListAsync();

                if (shoppingCarts.Count == 0)
                {
                    return NotFound();
                }

                var customer = await _customerContext.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return NotFound();
                }

                // update address if it's not equals

                if (JsonConvert.SerializeObject(customer.Address) != JsonConvert.SerializeObject(address))
                {
                    if (customer.Address == null)
                    {
                        customer.Address = new Address();
                    }
                    if (!String.IsNullOrEmpty(address.City))
                    {
                        customer.Address!.City = address.City;
                    }
                    if (!String.IsNullOrEmpty(address.Street))
                    {
                        customer.Address!.Street = address.Street;
                    }
                    if (!String.IsNullOrEmpty(address.HouseNr))
                    {
                        customer.Address!.HouseNr = address.HouseNr;
                    }
                    _customerContext.Customers.Update(customer);
                }

                decimal totalAmount = shoppingCarts.Sum(s => _productContext.Products.Find(s.ProductId)!.Price * _productContext.Products.Find(s.ProductId)!.AvailableQuantity);
                int discountPercentage = getDiscountPercentage(phoneNumber);
                Order o = new Order()
                {
                    CustomerId = customerId,
                    TotalAmount = totalAmount,
                    AppliedDiscount = discountPercentage > 0 ? totalAmount * discountPercentage / 100 : totalAmount
                };

                _context.Orders.Add(o);
                await _context.SaveChangesAsync();

                foreach (ShoppingCart sc in shoppingCarts)
                {
                    _orderItemContext.OrderItems.Add(
                        new OrderItem
                        {
                            OrderId = o.OrderId,
                            ProductId = sc.ProductId,
                            Quantity = sc.Quantity
                        }
                     );
                }
                await _orderItemContext.SaveChangesAsync();

                foreach (ShoppingCart sc in shoppingCarts)
                {
                    _shoppingCartContext.ShoppingCarts.Remove(sc);
                }

                await _shoppingCartContext.SaveChangesAsync();

                return CreatedAtAction("GetOrder", new { id = o.OrderId }, o);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int getDiscountPercentage(string phoneNumber)
        {
            int discountPercentage = 0;
            TimeSpan start = new TimeSpan(16, 0, 0);
            TimeSpan end = new TimeSpan(17, 0, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                int lastNumber = Int32.Parse(phoneNumber.Last().ToString());
                if (lastNumber == 0)
                {
                    discountPercentage = 30;
                }
                else if (lastNumber % 2 != 0)
                {
                    discountPercentage = 10;
                }
                else
                {
                    discountPercentage = 20;
                }
            }
            return discountPercentage;
        }
    }
}
