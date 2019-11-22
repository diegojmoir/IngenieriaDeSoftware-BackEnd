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
        private ProductDto _notCreatedValidDateProductDto;
        private Product _notCreatedNotValidDateProduct;
        private ProductDto _notCreatedNotValidDateProductDto;
        private Product _validProductModel;
        private Product _invalidProduct;
        private ProductDto _invalidProductDto;

        private DateTime NotValidDate = DateTime.Parse("11-12-2019");
        private const decimal ValidPrice = 2;
        private readonly DateTime _validDate = DateTime.Parse( "12-12-2019");
        private List<ProductDto> _validProducts;
        private List<ProductDto> _invalidProducts;
        private const string notExistingKey = "NOT EXISTING KEY";
        private const string ExistingKey = "EXISTING KEY";
        private Product _notValidModelProduct;
        [OneTimeSetUp]
        public void BeforeTests()
        {
            _invalidProducts = null;
            _validProducts = new List<ProductDto>();

            _validProductModel = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate,
            };
            _notValidModelProduct = new Product
            {
                Name = "",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate
            };
            _notCreatedNotValidDateProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = NotValidDate,
                EndingDate = _validDate,
            };
            _notCreatedNotValidDateProductDto = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = NotValidDate,
                EndingDate = _validDate,
                Key = "CLAVE"
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
                StartingDate = _validDate,
                EndingDate = _validDate,
                Key = _validProductKey
            };
            _notCreatedValidDateProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate,
            };
            _notCreatedValidDateProductDto = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate,
                Key = "CLAVE"
            };
            _invalidProduct = null;
            _invalidProductDto = null;
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
               .Setup(x => x.GetProducts())
               .Returns(_validProducts);
            _moqProductService
                .Setup(x => x.Delete(notExistingKey))
                .Returns(false);
            _moqProductService
                .Setup(x => x.Delete(ExistingKey))
                .Returns(true);
            _moqProductService
                .Setup(x => x.EditProduct(_notCreatedValidDateProductDto))
                .Returns(_invalidProductDto);

            _testController = new ProductsController(_moqProductService.Object);
        }
        [Test]
        public void Should_return_BadRequest_when_element_to_modify_is_not_found()
        {
            var result = _testController.Edit(_notCreatedValidDateProductDto) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void Should_return_true_if_product_to_delete_existed()
        {
            var result = _testController.Delete(ExistingKey) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, true);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Should_return_BadRequest_if_new_product_date_is_invalid()
        {
            var result = _testController.Create(_notCreatedNotValidDateProduct) as BadRequestObjectResult;
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_BadRequest_if_modified_product_date_is_invalid()
        {
            var result = _testController.Edit(_notCreatedNotValidDateProductDto) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_BadRequest_if_creating_null_product()
        {
            var result = _testController.Create(_invalidProduct) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_BadRequest_if_modifying_null_product()
        {
            var result = _testController.Edit(_invalidProductDto) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_BadRequest_if_product_model_is_not_valid()
        {
            var result = _testController.Create(_notValidModelProduct) as BadRequestObjectResult;
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_false_if_product_to_delete_not_existed()
        {
            var result = _testController.Delete(notExistingKey) as NotFoundObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, false);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Should_return_OkResult_if_there_are_products()
        {
            var result = _testController.GetProducts() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Should_return_OkResult_if_there_are_products_available()
        {
            var result = _testController.GetAvailable() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
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
        public void Should_return_bad_request_if_modified_product_is_not_valid()
        {
            var result = _testController.Edit(_invalidProductDto) as BadRequestObjectResult;
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
