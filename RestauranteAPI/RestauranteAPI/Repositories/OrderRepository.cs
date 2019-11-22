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

        public Order GetOrderFromStorage(Guid? ID)
        {
            var orders = Context.Orders.Where(x => x.ID == ID);
            if(orders.ToList().Count == 0) { return null; }
            var order = orders.First();
            return order;
        }

        public Order UpdateOrderInStorage(Order order)
        {
            Context.Orders.Update(order);
            Context.SaveChanges();
            return order;
        }
    }
}
