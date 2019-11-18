using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IOrderRepository
    {
        Order CreateOrderInStorage(Order order);
        FirebaseObject<Order> UpdateOrderInStorage(OrderDto order);
        IEnumerable<FirebaseObject<Order>> GetOrdersFromStorageByStatus(string status);
    }
}
