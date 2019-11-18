using Firebase.Database;
using RestauranteAPI.Configuration.Scaffolding;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using System;
using System.Collections.Generic;

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

        public FirebaseObject<Order> CrerateOrderInStorage(Order order)
        {
            throw new NotImplementedException();
        }

        public FirebaseObject<Order> DeleteOrderInStorage(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FirebaseObject<Order>> GetOrdersFromStorageByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public FirebaseObject<Order> UpdateOrderInStorage(OrderDto order)
        {
            throw new NotImplementedException();
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
