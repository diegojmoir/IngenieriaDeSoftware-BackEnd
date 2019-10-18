using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IProductRepository
    {
        FirebaseObject<Product> CrerateProductInStorage(Product product);
        IEnumerable<FirebaseObject<Product>> GetAvailableProductFromStorage();
    }
}
