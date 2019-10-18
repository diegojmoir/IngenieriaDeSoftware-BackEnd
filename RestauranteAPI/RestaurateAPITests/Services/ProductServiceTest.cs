using System;
using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Services;
using RestauranteAPI.Repositories.Injections;
using NUnit.Framework;
using Moq;
using Firebase.Database;
using Firebase.Database.Streaming;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;
using RestauranteAPI.Models.Dto;

namespace RestaurateAPITests.Services
{
    [TestFixture]
    public class ProductServiceTest
    {
        private ProductService _productService;
        private Mock<IProductRepository> _moqRepository;
        private const string validDate = "10-09-2020";
        private const double validPrice = 255.2;
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private FirebaseObject<Product> _validFirebaseObject;
        private List<ProductDto> _validFirebaseObjects;
        private Product _nonCreatedValidProduct;
        private Product _invalidProduct;
        private Product _validDatabaseModel;
        private ProductDto _validProductDto;


        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _validFirebaseObjects = new List<ProductDto>();

            _validProductDto = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = validPrice,
                IsAvailable = true,
                StartingDate = validDate,
                EndingDate = validDate,
                Key = Guid.NewGuid().ToString()
            };

            _nonCreatedValidProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = validPrice,
                IsAvailable = true,
                StartingDate = validDate,
                EndingDate = validDate

            };

            _validDatabaseModel = new Product
            {

                Name = _nonCreatedValidProduct.Name,
                Description = _nonCreatedValidProduct.Description,
                Price = _nonCreatedValidProduct.Price,
                IsAvailable = _nonCreatedValidProduct.IsAvailable,
                StartingDate = _nonCreatedValidProduct.StartingDate,
                EndingDate = _nonCreatedValidProduct.EndingDate
            };

            _validFirebaseObject = new FirebaseEvent<Product>(_validUserKey, _validDatabaseModel
                , FirebaseEventType.InsertOrUpdate, FirebaseEventSource.Offline);

            _validFirebaseObjects.Add(_validProductDto);

            _invalidProduct = null;
            _moqRepository = new Mock<IProductRepository>();
            _moqRepository.Setup(x => x.CrerateProductInStorage(_nonCreatedValidProduct))
                .Returns(_validFirebaseObject);

            


            var myMapper = new MapperConfiguration(x => { x.AddProfile(new MappingProfile()); }).CreateMapper();
            _productService = new ProductService(_moqRepository.Object, myMapper);
        }

        [Test]
        public void Should_return_valid_object_when_creating_new_user_and_new_product__is_valid()
        {
            var result = _productService.CreateProduct(_nonCreatedValidProduct);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUserKey, result.Key);
        }

        [Test]
        public void Should_return_valid_object_if_there_is_at_least_one_available_product()
        {
            var result = _productService.GetAvailable();
            Assert.IsNotNull(result);
        }








    }
}
