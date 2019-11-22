using System;
using System.Collections.Generic;
using System.Linq;
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
        private Product _nonCreatedValidProduct;
        private Product _validDatabaseModel;
        private readonly Product _notValidProduct=new Product();
        private ProductDto _validProductDto;
        private readonly string _validProductKey = Guid.NewGuid().ToString();
        private readonly string _invalidProductKey = Guid.NewGuid().ToString();


        [OneTimeSetUp]
        public void BeforeEachTest()
        {

            _validProductDto = new ProductDto
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = 255.2m,
                IsAvailable = true,
                StartingDate = _validDate,
                EndingDate = _validDate,
                Key = Guid.NewGuid().ToString()
            };

            _nonCreatedValidProduct = new Product
            {
                Name = "Some Name",
                Description = "Some Description",
                Price = 255.2m,
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


        [Test]
        public void should_return_null_if_creating_invalid_product()
        {
            _moqRepository.Setup(x => x.CreateProductInStorage(_notValidProduct));
            var result = _productService.CreateProduct(_notValidProduct);
            Assert.IsNull(result);
        }

        [Test]
        public void should_return_true_if_product_deleting_succeeds()
        {
            _moqRepository.Setup(x => x.DeleteProduct(_validProductKey))
                .Returns(true);
            var result = _productService.Delete(_validProductKey);
            Assert.IsTrue(result);
        }

        [Test]
        public void should_get_products_when_they_exist_in_database()
        {
            _moqRepository.Setup(x => x.GetProductsFromStorage())
                .Returns(new List<Product>
                {
                    new Product(),
                    new Product(),
                    new Product()
                });
            var result = _productService.GetProducts();
            var productDtos = result.ToList();
            Assert.IsTrue(productDtos.Count()==3);
            Assert.IsNotEmpty(productDtos);
        }

        [Test]
        public void should_get_product_from_database_if_this_match_with_input_guid()
        {
            _moqRepository.Setup(x => x.GetProductFromStorage(Guid.Parse(_validProductKey)))
                .Returns(_nonCreatedValidProduct);
            var result = _productService.GetProduct(Guid.Parse(_validProductKey));
            Assert.IsNotNull(result);
            Assert.AreEqual("Some Name",result.Name);
        }

        [Test]
        public void should_return_null_when_there_are_no_products_for_valid_guid()
        {
            _moqRepository.Setup(x => x.GetProductFromStorage(Guid.Parse(_invalidProductKey)));
            var result = _productService.GetProduct(Guid.Parse(_invalidProductKey));
            Assert.IsNull(result);
        }

        [Test]
        public void should_return_valid_product_when_updating_succeeds()
        {
            _moqRepository.Setup(x => x.UpdateProductInStorage(It.IsAny<Product>()))
                .Returns(_validDatabaseModel);
            var result = _productService.EditProduct(_validProductDto);
            Assert.IsNotNull(result);
        }

        [Test]
        public void should_returns_null_when_product_update_fails()
        {
            _moqRepository.Setup(x => x.UpdateProductInStorage(It.IsAny<Product>()));
            var result = _productService.EditProduct(_validProductDto);
            Assert.IsNull(result);
        }






    }
}
