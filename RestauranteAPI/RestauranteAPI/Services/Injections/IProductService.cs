using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    public interface IProductService
    {
        UserDto GetProduct(string id);
        UserDto CreateProduct(Product product); 
    }
}
