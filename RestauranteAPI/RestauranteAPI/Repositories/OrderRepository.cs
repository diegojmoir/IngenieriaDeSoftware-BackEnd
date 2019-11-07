using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using System;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public FirebaseObject<Order> CreateOrderInStorage(Order order)
        {
            throw new NotImplementedException();
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
    }
}
