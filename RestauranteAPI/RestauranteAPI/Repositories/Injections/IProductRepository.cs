using System.Threading.Tasks;
using RestauranteAPI.Models;
using Firebase.Database;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IProductRepository
    {
        FirebaseObject<User> GetProductFromStorageById(string id);
        FirebaseObject<User> CrerateProductInStorage(Product product);
    }
}
