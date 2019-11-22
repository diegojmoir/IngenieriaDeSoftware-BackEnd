using Firebase.Database;
using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Configuration.Scaffolding;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private RestauranteDbContext Context { get; set; }
        public OrderRepository(RestauranteDbContext context)
        {
            Context = context;
        }
        public Order CreateOrderInStorage(Order order)
        {
            Context.Orders.Add(order);
            order.ProductsOrdered = new List<OrderedProduct>(); 
            Context.SaveChanges();
            CreateOrderedProducts(order.ID, order.ProductsOrdered, order.Products);
            Context.OrderedProducts.AddRange(order.ProductsOrdered);
            Context.SaveChanges();
            return order;
        }

        public bool DeleteOrderInStorage(Order order)
        {
            var ProductsOrdered = Context.OrderedProducts.Where(p => p.ID_Order == order.ID).ToList();
            foreach (var product in ProductsOrdered) 
            {
                Context.OrderedProducts.Remove(product);
            }
            Context.SaveChanges();
            var order1 = new Order { ID = order.ID };
            Context.Orders.Attach(order1);
            Context.Orders.Remove(order1);
            Context.SaveChanges();
            return true;
        }

        public Order GetOrderFromStorage(Guid? ID)
        {
            var orders = Context.Orders.Where(x => x.ID == ID);
            if (orders.ToList().Count == 0) { return null; }
            var order = orders.First();
            var products = Context.OrderedProducts.Where(x => x.ID_Order == ID).ToList();
            var products_ids = new List<Guid>();
            foreach (var p in products)
            {
                products_ids.Add((Guid)p.ID_Product);   
            }
            order.Products = products_ids.ToArray();
            return order;
        }

        public Order UpdateOrderInStorage(Order order)
        {
            Context.Orders.Update(order);
            Context.SaveChanges();
            return order;
        }

        private static void CreateOrderedProducts(Guid? orderId, ICollection<OrderedProduct> orderedProducts, IEnumerable<Guid> products)
        {
            foreach (var productId in products)
            {
                orderedProducts.Add(new OrderedProduct
                {
                    ID_Product = productId,
                    ID_Order = orderId
                });
            }
        }
    }
}
