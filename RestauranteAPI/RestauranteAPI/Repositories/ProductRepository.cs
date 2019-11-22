using System;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using System.Linq;
using System.Collections.Generic;
using RestauranteAPI.Configuration.Scaffolding;

namespace RestauranteAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private RestauranteDbContext Context { get; set; }

        public ProductRepository(RestauranteDbContext context)
        {
            Context = context;
        }

        public Product CreateProductInStorage(Product product)
        {
            Context.Products.Add(product);
            product.ProductCategories=new List<ProductCategory>();
            Context.SaveChanges();
            CreateProductCategories(product.ID,product.ProductCategories,product.Categories);
            Context.ProductCategories.AddRange(product.ProductCategories);
            Context.SaveChanges();
            return product;
        }

        public bool DeleteProduct(string key)
        {
            
                Context.ProductCategories
                    .RemoveRange(Context.ProductCategories.Where(x=>x.ID_Product.ToString()==key));
                Context.SaveChanges();
                Context.Products.RemoveRange(Context.Products.Where(x=>x.ID==Guid.Parse(key)));
                Context.SaveChanges();
                return true;

        }

        public IEnumerable<Product> GetAvailableProductFromStorage()
        {
            var resultSet = Context.Products.Where(x => x.IsAvailableNow()).ToList();
            resultSet.ForEach(x =>
                {
                    x.Categories = Context.ProductCategories.Where(y => y.ID_Product == x.ID)
                        .Select(y => y.ID_Category).ToArray();
                });
            return resultSet;
        }

        public Product UpdateProductInStorage(Product product)
        {
            var newProductCategoriesCategories = new List<ProductCategory>();
            CreateProductCategories(product.ID, newProductCategoriesCategories, product.Categories);
            Context.RemoveRange( Context.ProductCategories.Where(x=>x.ID_Product==product.ID));
            Context.Products.Update(product);
            Context.ProductCategories.AddRange(newProductCategoriesCategories);
            Context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetProductsFromStorage()
        {
            var resultSet = Context.Products.ToList();
            resultSet.ForEach(x =>
            {
                x.Categories = Context.ProductCategories.Where(y => y.ID_Product == x.ID)
                    .Select(y => y.ID_Category).ToArray();
            });
            return resultSet;
        }

        private static void CreateProductCategories(Guid?productId,ICollection<ProductCategory> productCategories,IEnumerable<int?> categories)
        {
            foreach (var categoryId in categories)
            {
                productCategories.Add(new ProductCategory
                {
                    ID_Category = categoryId,
                    ID_Product = productId
                });
            }
        }

        public Product GetProductFromStorage(Guid? key)
        {
            var result = Context.Products
                .FirstOrDefault(x => x.ID == key);
            return result;
        }
    }
}