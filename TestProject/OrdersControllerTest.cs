using ECommerceApplication.Controllers;
using ECommerceApplication.Models;
using ECommerceApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class OrdersControllerTest
    {
        private ProductContext productDbContext;
        private ShoppingCartContext shoppingCartDbContext;
        private OrderContext orderDbContext;
        private OrderItemContext orderItemDbContext;
        private CustomerContext customerDbContext;

        [SetUp]
        public void Setup()
        {
            ProductDbContext pdb = new ProductDbContext();
            productDbContext = pdb.GetProductContext();
            ShoppingCartDbContext scdb = new ShoppingCartDbContext();
            shoppingCartDbContext = scdb.GetShoppingCartContext();
            OrderDbContext odb = new OrderDbContext();
            orderDbContext = odb.GetOrderContext();
            OrderItemDbContext oidb = new OrderItemDbContext();
            orderItemDbContext = oidb.GetOrderItemContext();
            CustomerDbContext cdb = new CustomerDbContext();
            customerDbContext = cdb.GetCustomerContext();
        }

        [Test]
        public async Task Should_Return_Orders_When_Calling_Get_Without_Parameters()
        {
            OrdersController ordersController = new OrdersController(orderDbContext, orderItemDbContext, shoppingCartDbContext, productDbContext, customerDbContext);
            //Act
            var orders = await ordersController.GetOrders();
            //Assert
            Assert.NotNull(orders.Value);
        }

        [Test]
        public async Task Should_Return_Order_When_Calling_Get_With_Id_Paramete()
        {
            OrdersController ordersController = new OrdersController(orderDbContext, orderItemDbContext, shoppingCartDbContext, productDbContext, customerDbContext);
            //Act
            var order = await ordersController.GetOrder(1);
            //Assert
            Assert.NotNull(order.Value);
        }



        [Test]
        public async Task Should_Return_Order_When_Calling_Post()
        {
            OrdersController ordersController = new OrdersController(orderDbContext, orderItemDbContext, shoppingCartDbContext, productDbContext, customerDbContext);
            //Act
            Address address = new Address(){
                AddressId = 1,
                City = "Beograd",
                Street = "Milutina Milankovica",
                HouseNr = "22"
                };
            var order = await ordersController.PostOrder(1, "0601001001", address);
            //Assert
            Assert.NotNull((order.Result as CreatedAtActionResult)?.Value);
        }
}
}