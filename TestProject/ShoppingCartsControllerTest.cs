using ECommerceApplication.Controllers;
using ECommerceApplication.Models;
using ECommerceApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class ShoppingCartsControllerTest
    {
        private ProductContext productDbContext;
        private ShoppingCartContext shoppingCartDbContext;

        [SetUp]
        public void Setup()
        {
            ProductDbContext pdb = new ProductDbContext();
            //Arrange
            productDbContext = pdb.GetProductContext();
            ShoppingCartDbContext scdb = new ShoppingCartDbContext();
            shoppingCartDbContext = scdb.GetShoppingCartContext();
        }

        [Test]
        public async Task Should_Return_ShoppingCart_When_Calling_Get_With_Id_Paramete()
        {
            ShoppingCartsController shoppingCartsController = new ShoppingCartsController(shoppingCartDbContext, productDbContext);
            //Act
            var shoppingCarts = await shoppingCartsController.GetShoppingCart(1);
            //Assert
            Assert.NotNull(shoppingCarts.Value);
        }

        [Test]
        public async Task Should_Return_ShoppingCart_When_Calling_Get_With_CustomerId_Parameter()
        {
            ShoppingCartsController shoppingCartsController = new ShoppingCartsController(shoppingCartDbContext, productDbContext);
            //Act
            var shoppingCarts = await shoppingCartsController.GetShoppingCartsByCustomerId(1);
            //Assert
            Assert.NotNull(shoppingCarts.Value);
        }

        [Test]
        public async Task Should_Return_ShoppingCart_When_Calling_Post()
        {
            ShoppingCartsController shoppingCartsController = new ShoppingCartsController(shoppingCartDbContext, productDbContext);

            //Act
            var shoppingCart = await shoppingCartsController.PostShoppingCart(1,2,10);
            //Assert
            Assert.NotNull((shoppingCart.Result as CreatedAtActionResult)?.Value);
        }
    }
}