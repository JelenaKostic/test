using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApplication.Models;
using Moq;
using ECommerceApplication.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        private readonly ProductContext _productContext;

        public ShoppingCartsController(ShoppingCartContext context, ProductContext productContext)
        {
            _context = context;
            _productContext = productContext;
        }

        // GET: api/ShoppingCarts/5 --> needed for post
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(long id)
        {
            try
            {
                var shoppingCart = await _context.ShoppingCarts.FindAsync(id);

                if (shoppingCart == null)
                {
                    return NotFound("This Shopping cart does not exist");
                }

                return shoppingCart;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ShoppingCarts/Customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<List<ShoppingCart>>> GetShoppingCartsByCustomerId(long customerId)
        {
            try
            {
                var shoppingCarts = await _context.ShoppingCarts.Where(sc => sc.CustomerId == customerId).ToListAsync();

                if (shoppingCarts.Count == 0)
                {
                    return NotFound("There is no Shopping carts for this customer");
                }

                return shoppingCarts;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/ShoppingCarts
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart(long customerId, long productId, int quantity)
        {
            try
            {
                ShoppingCart? shoppingCart = _context.ShoppingCarts.FirstOrDefault(s => s.ProductId == productId && s.CustomerId == customerId);

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart()
                    {
                        CustomerId = customerId,
                        ProductId = productId,
                        Quantity = quantity
                    };
                }
                else
                {
                    return BadRequest("Product already exists in shopping cart!");
                }

                Product? product = await _productContext.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound("This product not exist!");
                }
                if (product.AvailableQuantity < quantity)
                {
                    // check at supplier available quantity
                    var supplierProductMock = new Mock<IProductService>();
                    supplierProductMock.Setup(item => item.AvailableQuantity()).Returns(10);
                    if (product.AvailableQuantity + supplierProductMock.Object.AvailableQuantity() < quantity)
                    {
                        return BadRequest("We don't have enough of the desired product!");
                    }
                }

                _context.ShoppingCarts.Add(shoppingCart);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.ShoppingCartId }, shoppingCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
