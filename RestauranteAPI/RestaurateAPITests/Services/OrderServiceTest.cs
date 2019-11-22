using AutoMapper;
using Firebase.Database;
using Firebase.Database.Streaming;
using Moq;
using NUnit.Framework;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RestaurateAPITests.Services
{
    class OrderServiceTest
    {
        private OrderService _OrderService;
        private Mock<IOrderRepository> _moqRepository;
        private const string validDate = "10-09-2020";
        private const double validPrice = 255.2;
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private FirebaseObject<Order> _validFirebaseObject;
        private List<OrderDto> _validFirebaseObjects;
        private Order _nonCreatedValidOrder;
        private Order _invalidOrder;
        private Order _validDatabaseModel;
        private OrderDto _validOrderDto;

        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _validFirebaseObjects = new List<OrderDto>();

            _validOrderDto = new OrderDto
            {
                Key = Guid.NewGuid().ToString(),
                Date = validDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
            };

            _nonCreatedValidOrder = new Order
            {
                ID = Guid.NewGuid(),
                Date = validDate,
                Client = "Some client key",
                Status = "pending",
                ProductsOrdered = new Collection<Product>()
            };

            _validDatabaseModel = new Order
            {
                ID = _nonCreatedValidOrder.ID,
                Date = _nonCreatedValidOrder.Date,
                Client = _nonCreatedValidOrder.Client,
                Status = _nonCreatedValidOrder.Status,
                ProductsOrdered = _nonCreatedValidOrder.ProductsOrdered
            };
        }

    }
}
