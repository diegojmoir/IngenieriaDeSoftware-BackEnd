using System;
using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Services;
using RestauranteAPI.Repositories.Injections;
using NUnit.Framework;
using Moq;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Models.Dto;

namespace RestaurateAPITests.Services
{
    [TestFixture]
    public class ProductServiceTest
    {
        private ProductService _productService;
        private Mock<IProductRepository> _moqRepository;
        private readonly DateTime _validDate = DateTime.Parse("10-09-2020");
        private const decimal ValidPrice = 255.2m;
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private Product _nonCreatedValidProduct;
        private Product _validDatabaseModel;
        private ProductDto _validProductDto;


        [OneTimeSetUp]
        public void BeforeEachTest()
        {

            _validProductDto = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate,
                Key = Guid.NewGuid().ToString()
            };

            _nonCreatedValidProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = ValidPrice,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate

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
            _moqRepository = new Mock<IProductRepository>();
            _moqRepository.Setup(x => x.CreateProductInStorage(_nonCreatedValidProduct))
                .Returns(_validDatabaseModel);
            var myMapper = new MapperConfiguration(x => { x.AddProfile(new MappingProfile()); }).CreateMapper();
            _productService = new ProductService(_moqRepository.Object, myMapper);
        }

        [Test]
        public void Should_return_valid_object_when_creating_new_user_and_new_product__is_valid()
        {
            var result = _productService.CreateProduct(_nonCreatedValidProduct);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_return_valid_object_if_there_is_at_least_one_available_product()
        {
            var result = _productService.GetAvailable();
            Assert.IsNotNull(result);
        }








    }
}
