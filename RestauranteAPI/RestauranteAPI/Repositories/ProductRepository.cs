using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
using System.Collections.Generic;
using RestauranteAPI.Models.Dto;

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

        public bool DeleteProduct(string key)
        {
            FirebaseConfig.FirebaseStartUp().Wait();

            // TODO: Modificación por SQLServer
            throw new System.NotImplementedException();
               
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("ProductsCollection")
                    .Child("Products")
                    .Child(key)
                    .DeleteAsync();
                while (!response.IsCompleted) ;
                return response.IsCompletedSuccessfully;
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

        public FirebaseObject<Product> UpdateProductInStorage(ProductDto product)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var fbProduct = FirebaseConfig.FirebaseClient)
            {
                var responseToUpdate = fbProduct.Child("ProductsCollection").Child("Products").Child(product.Key).PutAsync<ProductDto>(product);

                var response = fbProduct
                    .Child("ProductsCollection")
                    .Child("Products")
                    .OnceAsync<Product>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Name == product.Name));
                    return response;                
            }
        }



        public IEnumerable<FirebaseObject<Product>> GetProductsFromStorage()
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("ProductsCollection")
                    .Child("Products")
                    .OnceAsync<Product>()
                    .Result
                    .Where(x => x.Object != null);
                return response;
            }
        }

    }
}