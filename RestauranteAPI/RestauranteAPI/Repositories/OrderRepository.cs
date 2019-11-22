using Firebase.Database;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersFromStorageByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrderInStorage(Order order)
        {
            Context.Orders.Update(order);
            Context.SaveChanges();
            return order;
        }
    }
}
