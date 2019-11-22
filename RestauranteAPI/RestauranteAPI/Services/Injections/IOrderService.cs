using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using System;
using System.Collections.Generic;

namespace RestauranteAPI.Services.Injections
{
    public interface IOrderService
    {
        OrderDto CreateOrder(Order product);
        OrderDto EditOrder(OrderDto order);
        OrderDto GetOrder(Guid? ID);
    }
}
