using System.Collections.Generic;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Services.Injections
{
    public interface IProductService
    {
        ProductDto CreateProduct(Product product);
        IEnumerable<ProductDto> GetAvailable();
        ProductDto EditProduct(ProductDto product);
        bool Delete(string key);
        IEnumerable<ProductDto> GetProducts();
    }
}
