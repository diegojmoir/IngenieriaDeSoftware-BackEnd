using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Dto;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories.Injections;
using System.Collections.Generic;
using Firebase.Database;

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
            var resultObject = _productRepository.CrerateProductInStorage(product);
            if(resultObject == null)
            {
                return null;
            }
            var result = new ProductDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public ProductDto GetProduct(string id)
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerable<ProductDto> GetAvailable()
        {
            IEnumerable<FirebaseObject<Product>> resultObjects = _productRepository.GetAvailableProductFromStorage();
            if (resultObjects == null)
                return null;
            

            List <ProductDto> resultsObjectList = new List<ProductDto>();
            foreach (FirebaseObject<Product> resultObject in resultObjects)
            {
                var resultTmp = new ProductDto();
                resultTmp = _mapper.Map(resultObject, resultTmp);
                resultsObjectList.Add(resultTmp);
            }
            return resultsObjectList;
        }
    }
}