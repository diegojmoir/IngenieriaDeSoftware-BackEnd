using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System;
using System.Collections.Generic;

namespace RestauranteAPI.Repositories.Injections
{
    public interface IOrderRepository
    {
        Order CreateOrderInStorage(Order order);
        Order UpdateOrderInStorage(Order order);
        Order GetOrderFromStorage(Guid? ID);
    }
}
