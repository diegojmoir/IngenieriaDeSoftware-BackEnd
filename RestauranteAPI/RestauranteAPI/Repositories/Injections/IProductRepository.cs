using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database;
using System.Collections.Generic;
using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IProductRepository
    {
        FirebaseObject<Product> CrerateProductInStorage(Product product);
        IEnumerable<FirebaseObject<Product>> GetAvailableProductFromStorage();
        FirebaseObject<Product> UpdateProductInStorage(ProductDto product);
        bool DeleteProduct(string key);
        IEnumerable<FirebaseObject<Product>> GetProductsFromStorage();
    }
}
