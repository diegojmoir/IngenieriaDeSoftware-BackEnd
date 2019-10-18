using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public FirebaseObject<Product> CrerateProductInStorage(Product product)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using(var client = FirebaseConfig.FirebaseClient)
            {
                var response = client.Child("ProductsCollection")
                    .Child("Products")
                    .PostAsync(product, false)
                    .Result;
                return response;
            }
        }

        public IEnumerable<FirebaseObject<Product>> GetAvailableProductFromStorage()
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using(var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("ProductsCollection")
                    .Child("Products")
                    .OnceAsync<Product>()
                    .Result
                    .Where(x => x.Object != null && (x.Object.IsAvailable) && x.Object.IsAvailableNow());
                return response;
            }
        }

        public FirebaseObject<Product> GetProductFromStorageById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}