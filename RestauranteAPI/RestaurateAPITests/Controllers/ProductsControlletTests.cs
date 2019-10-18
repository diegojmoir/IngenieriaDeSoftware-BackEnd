using RestauranteAPI.Controllers;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Models;
using NUnit.Framework;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models.Dto;
using System.Collections.Generic;

namespace RestaurateAPITests.Controllers
{

    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _testController;
        private Mock<IProductService> _moqProductService;
        private readonly string _validProductKey = Guid.NewGuid().ToString();
        private ProductDto _validProduct;
        private ProductDto _notValidDateProduct;
        private Product _notCreatedValidDateProduct;
        private Product _notCreatedNotValidDateProduct;
        private Product _validProductModel;
        private Product _invalidProduct;
        private const string NotValidDate = "111-111-111";
        private const double ValidPrice = 2;
        private const string ValidDate = "12-12-2019";
        private List<ProductDto> _validProducts;

        [OneTimeSetUp]
        public void BeforeTests()
        {
            _validProducts = new List<ProductDto>();

            _validProductModel = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = ValidDate,
                EndingDate = ValidDate,
            };
            _notValidDateProduct = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = NotValidDate,
                EndingDate = NotValidDate,
                Key = Guid.NewGuid().ToString()
            };
            _validProduct = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = ValidDate,
                EndingDate = ValidDate,
                Key = _validProductKey
            };
            _notCreatedValidDateProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = ValidDate,
                EndingDate = ValidDate,
            };
            _invalidProduct = null;
            _moqProductService = new Mock<IProductService>();
            _moqProductService
                .Setup(x => x.GetAvailable()).
                Returns(_validProducts);
            _moqProductService
                .Setup(x => x.CreateProduct(_notCreatedValidDateProduct))
                .Returns(_validProduct);

            _moqProductService
                .Setup(x => x.CreateProduct(_validProductModel))
                .Returns(_validProduct);

            _moqProductService
                .Setup(x => x.CreateProduct(_notCreatedNotValidDateProduct))
                .Returns(_notValidDateProduct);
            //_moqUserService
            //    .Setup(x => x.CreateUser(_alreadyExistingUsernameUser))
            //   .Returns(_validUser);

            _testController = new ProductsController(_moqProductService.Object);
        }
        

     


        [Test]
        public void Should_return_OkResult_if_new_user_is_valid()
        {
            var result = _testController.Create(_notCreatedValidDateProduct) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void User_key_shall_be_valid_if_created_user_is_valid()
        {
            var result = _testController.Create(_validProductModel) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(_validProductKey, ((ProductDto)result.Value).Key);
        }

        [Test]
        public void Should_return_bad_request_if_new_user_is_not_valid()
        {
            var result = _testController.Create(_invalidProduct) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }



        [Test]
        public void Should_return_bad_request_if_not_valid_product()
        {
            var result = _testController.Create(_invalidProduct) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
