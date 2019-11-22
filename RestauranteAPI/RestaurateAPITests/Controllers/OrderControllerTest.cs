using RestauranteAPI.Controllers;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Models;
using NUnit.Framework;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models.Dto;
using System.Collections.Generic;
using RestauranteAPI.Models.Mapping;
using System.Collections.ObjectModel;

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
        private const double ValidPrice = 2;
        private const string ValidDate = "12-12-2019";
        private List<OrderDto> _validOrders;

         [OneTimeSetUp]
        public void BeforeTests()
        {
            _validOrders = new List<OrderDto>();

            _validOrderModel = new Order
            {
                ID = Guid.NewGuid(),
                Date = ValidDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
            };
            _notValidDateOrder = new OrderDto
            {
                Key = Guid.NewGuid().ToString(),
                Date = NotValidDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
            };
            _validOrder = new OrderDto
            {
                Key = Guid.NewGuid().ToString(),
                Date = ValidDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
            };
            _notCreatedValidDateOrder = new Order
            {
                ID = Guid.NewGuid(),
                Date = NotValidDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
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
            _testController = new OrdersController(_moqOrderService.Object);
        }
  
    }
}
