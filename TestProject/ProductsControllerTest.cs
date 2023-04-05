using ECommerceApplication.Controllers;
using ECommerceApplication.Models;
using ECommerceApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class ProductsControllerTest
    {
        private ProductContext dbContext;

        [SetUp]
        public void Setup()
        {
            ProductDbContext pdb = new ProductDbContext();
            //Arrange
            dbContext = pdb.GetProductContext();
        }

        [Test]
        public async Task Should_Return_All_Products_When_Calling_Get_Without_Parameters()
        {
            ProductsController productController = new ProductsController(dbContext);
            //Act
            var products = await productController.GetProducts();
            //Assert
            Assert.NotNull(products.Value);
        }

        [Test]
        public async Task Should_Return_Product_When_Calling_Get_With_Id_Parameter()
        {
            ProductsController productController = new ProductsController(dbContext);
            //Act
            var product = await productController.GetProduct(1);
            //Assert
            Assert.NotNull(product.Value);
        }

        [Test]
        public async Task Should_Return_Product_When_Calling_Post()
        {
            ProductsController productController = new ProductsController(dbContext);
            Product productToAdd = new Product()
            {
                ProductId = 11,
                ProductName = "Product 11",
                AvailableQuantity = 110,
                Price = 10
            };

            //Act
            var product = await productController.PostProduct(productToAdd);
            //Assert
            Assert.NotNull((product.Result as CreatedAtActionResult)?.Value);
        }
    }
}