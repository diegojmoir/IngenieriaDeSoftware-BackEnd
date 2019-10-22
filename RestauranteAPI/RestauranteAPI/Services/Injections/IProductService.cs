using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    using System.Collections.Generic;

    public interface IProductService
    {
        ProductDto CreateProduct(Product product);
        IEnumerable<ProductDto> GetAvailable();
        ProductDto EditProduct(ProductDto product);
    }
}
