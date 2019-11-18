using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System.Collections.Generic;
using RestauranteAPI.Models.Dto;

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
