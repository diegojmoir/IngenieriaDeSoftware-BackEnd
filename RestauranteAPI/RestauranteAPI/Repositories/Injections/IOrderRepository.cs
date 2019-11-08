using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IOrderRepository
    {
        FirebaseObject<Order> CreateOrderInStorage(Order order);
        FirebaseObject<Order> UpdateOrderInStorage(OrderDto order);
        FirebaseObject<Order> DeleteOrderInStorage(OrderDto order);
        IEnumerable<FirebaseObject<Order>> GetOrdersFromStorageByStatus(string status);
    }
}
