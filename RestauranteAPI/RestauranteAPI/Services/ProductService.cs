using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Dto;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories.Injections;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDto CreateProduct(Product product)
        {
            var resultObject = _productRepository.CreateProductInStorage(product);
            if(resultObject == null)
            {
                return null;
            }
            var result = new ProductDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public bool Delete(string key)
        {
            bool resultBool = _productRepository.DeleteProduct(key);
            return resultBool;
        }

        public ProductDto EditProduct(ProductDto product)
        {
            var domainModel = _mapper.Map<Product>(product);

            var resultObject = _productRepository
                .UpdateProductInStorage(domainModel);
            if (resultObject == null)
                return null;
            var result = new ProductDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public IEnumerable<ProductDto> GetAvailable()
        {
            var resultObjects = _productRepository.GetAvailableProductFromStorage();
            return MapProductsDtos(resultObjects);
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            var resultObjects = _productRepository.GetProductsFromStorage();
            return MapProductsDtos(resultObjects);

        }

        private  IEnumerable<ProductDto> MapProductsDtos(IEnumerable<Product> products)
        {
            var resultsObjectList = products.ToList().Select(x =>
            {
                var resultTmp = new ProductDto();
                return _mapper.Map(x, resultTmp);
            });
            return resultsObjectList;
        }

        
    }
}