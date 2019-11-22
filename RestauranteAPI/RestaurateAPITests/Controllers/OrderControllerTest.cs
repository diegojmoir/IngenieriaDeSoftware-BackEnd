using RestauranteAPI.Controllers;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Models;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using RestauranteAPI.Models.Mapping;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace RestaurateAPITests.Controllers
{
    [TestFixture]
    public class OrderControllerTest
    {
        private OrdersController _testController;
        private Mock<IOrderService> _moqOrderService;
        private readonly string _validOrderKey = Guid.NewGuid().ToString();
        private OrderDto _validOrder;
        private OrderDto _notValidDateOrder;
        private Order _notCreatedValidDateOrder;
        private Order _notCreatedNotValidDateOrder;
        private Order _validOrderModel;
        private Order _invalidOrder;
        private const string NotValidDate = "111-111-111";
        private readonly DateTime _validDate =DateTime.Parse("12-12-2019");
        private List<OrderDto> _validOrders;

         [OneTimeSetUp]
        public void BeforeTests()
        {
            _validOrders = new List<OrderDto>();

            _validOrderModel = new Order
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };
            _notValidDateOrder = new OrderDto
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };
            _validOrder = new OrderDto
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };
            _notCreatedValidDateOrder = new Order
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };
            _invalidOrder = null;
            _moqOrderService = new Mock<IOrderService>();
            _moqOrderService
                .Setup(x => x.CreateOrder(_notCreatedValidDateOrder))
                .Returns(_validOrder);
            _moqOrderService
               .Setup(x => x.CreateOrder(_validOrderModel))
               .Returns(_validOrder);
            _moqOrderService
                .Setup(x => x.CreateOrder(_notCreatedNotValidDateOrder))
                .Returns(_notValidDateOrder);
            _moqOrderService
                .Setup(x => x.DeleteOrder(_validOrderModel))
                .Returns(true);
            _moqOrderService
                .Setup(x => x.DeleteOrder(_notCreatedValidDateOrder))
                .Returns(false);
            _testController = new OrdersController(_moqOrderService.Object);
        }

        [Test]
        public void Should_return_true_if_order_to_delete_existed()
        {
            var result = _testController.Delete(_validOrderModel) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, "Orden Eliminada");
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public void Should_return_false_if_order_to_delete_doesnt_existed()
        {
            var result = _testController.Delete(_notCreatedValidDateOrder) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
