using RestauranteAPI.Models;
using System;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IProductRepository
    {
        Product CreateProductInStorage(Product product);
        IEnumerable<Product> GetAvailableProductFromStorage();
        Product UpdateProductInStorage(Product product);
        bool DeleteProduct(string key);
        IEnumerable<Product> GetProductsFromStorage();
        Product GetProductFromStorage(Guid? key);
    }
}
