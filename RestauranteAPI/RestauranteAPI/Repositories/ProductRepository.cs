using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
namespace RestauranteAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public FirebaseObject<User> CrerateProductInStorage(Product product)
        {
            throw new System.NotImplementedException();
        }

        public FirebaseObject<User> GetProductFromStorageById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}