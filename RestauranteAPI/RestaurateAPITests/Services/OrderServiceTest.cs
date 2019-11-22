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

namespace RestaurateAPITests.Services
{
    [TestFixture]
    class OrderServiceTest
    {
        private OrderService _OrderService;
        private Mock<IOrderRepository> _moqRepository;
        private const string validDate = "10-09-2020";
        private const double validPrice = 255.2;
        private readonly string _validOrderKey = Guid.NewGuid().ToString();
        private FirebaseObject<Order> _validFirebaseObject;
        private List<OrderDto> _validFirebaseObjects;
        private Order _nonCreatedValidOrder;
        private Order _invalidOrder;
        private Order _validDatabaseModel;
        private OrderDto _validOrderDto;
        private readonly DateTime _validDate = DateTime.Parse("12-12-2019");

        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _validFirebaseObjects = new List<OrderDto>();

            _validOrderDto = new OrderDto
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };

            _nonCreatedValidOrder = new Order
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };

            _validDatabaseModel = new Order
            {
                ID = Guid.NewGuid(),
                Date = _validDate,
                Client = "Some client key",
                Status = 1,
                ProductsOrdered = new Collection<OrderedProduct>()
            };

            _validFirebaseObject = new FirebaseEvent<Order>(_validOrderKey, _validDatabaseModel
                , FirebaseEventType.InsertOrUpdate, FirebaseEventSource.Offline);

            _validFirebaseObjects.Add(_validOrderDto);

            _invalidOrder = null;
            _moqRepository = new Mock<IOrderRepository>();
            _moqRepository.Setup(x => x.CreateOrderInStorage(_nonCreatedValidOrder)).Returns(_validDatabaseModel);

            var myMapper = new MapperConfiguration(x => { x.AddProfile(new MappingProfile()); }).CreateMapper();
            _OrderService = new OrderService(_moqRepository.Object, myMapper);


        }

        [Test]
        public void Should_return_valid_object_when_creating_new_order_and_new_order__is_valid()
        {
            var result = _OrderService.CreateOrder(_nonCreatedValidOrder);
            Assert.IsNotNull(result);
        }

        [Test]
        public void should_return_null_if_creating_invalid_order()
        {
            _moqRepository.Setup(x => x.CreateOrderInStorage(_invalidOrder));
            var result = _OrderService.CreateOrder(_invalidOrder);
            Assert.IsNull(result);
        }

        [Test]
        public void should_return_true_if_delete_of_orders_succeeds()
        {
            _moqRepository.Setup(x => x.DeleteOrderInStorage(_nonCreatedValidOrder))
                .Returns(true);
            var result = _OrderService.DeleteOrder(_nonCreatedValidOrder);
            Assert.True(result);
        }

        [Test]
        public void should_return_valid_model_when_order_exist_for_validId()
        {
            _moqRepository.Setup(x => x.GetOrderFromStorage(Guid.Parse(_validOrderKey)))
                .Returns(_nonCreatedValidOrder);
            var result = _OrderService.GetOrder(Guid.Parse(_validOrderKey));
            Assert.IsInstanceOf<OrderDto>(result);
        }

        [Test]
        public void should_returns_null_when_there_is_no_model_for_valid_Guid()
        {
            _moqRepository.Setup(x => x.GetOrderFromStorage(Guid.Parse(_validOrderKey)));
            var result = _OrderService.GetOrder(Guid.Parse(_validOrderKey));
            Assert.IsNull(result);
        }


        [Test]
        public void should_return_valid_model_when_update_status_of_orders_succeeds()
        {
            _moqRepository.Setup(x => x.UpdateOrderStatusInStorage(Guid.Parse(_validOrderKey),It.IsAny<int>() ))
                .Returns(_nonCreatedValidOrder);
            var result =_OrderService.EditOrderStatus(Guid.Parse(_validOrderKey),0);
            Assert.IsNotNull(result);
        }

        [Test]
        public void should_return_null_when_update_status_of_orders_fails()
        {

            _moqRepository.Setup(x => x.UpdateOrderStatusInStorage(Guid.Parse(_validOrderKey),It.IsAny<int>() ));
            var result = _OrderService.EditOrderStatus(Guid.Parse(_validOrderKey),0);
            Assert.IsNull(result);
        }

        [Test]
        public void should_return_valid_model_when_update_of_orders_succeeds()
        {
           _moqRepository.Setup(x => x.UpdateOrderInStorage(It.IsAny<Order>()))
                .Returns(_nonCreatedValidOrder);

            var result=_OrderService.EditOrder(_validOrderDto);
            Assert.IsNotNull(result);
        }

        [Test]
        public void should_return_null_when_update_of_orders_fails()
        {

            _moqRepository.Setup(x => x.UpdateOrderInStorage(It.IsAny<Order>()));
            var result=_OrderService.EditOrder(_validOrderDto);
            Assert.IsNull(result);
        }


    }
}
