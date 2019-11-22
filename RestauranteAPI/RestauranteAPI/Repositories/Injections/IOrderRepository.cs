using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IOrderRepository
    {
        Order CreateOrderInStorage(Order order);
        Order UpdateOrderInStorage(Order order);
        IEnumerable<Order> GetOrdersFromStorageByStatus(string status);
    }
}
