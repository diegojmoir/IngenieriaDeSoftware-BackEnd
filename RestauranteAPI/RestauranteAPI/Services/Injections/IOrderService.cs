using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;

namespace RestauranteAPI.Services.Injections
{
    public interface IOrderService
    {
        OrderDto CreateOrder(Order product);
        OrderDto EditOrder(OrderDto order);
        bool DeleteOrder(OrderDto order);
        IEnumerable<OrderDto> GetOrdersByStatus(string status);
    }
}
