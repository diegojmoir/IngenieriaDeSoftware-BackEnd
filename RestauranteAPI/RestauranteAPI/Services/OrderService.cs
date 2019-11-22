using AutoMapper;
using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services.Injections;
using System;
using System.Collections.Generic;

namespace RestauranteAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public OrderDto CreateOrder(Order order)
        {
            var resultObject = _orderRepository.CreateOrderInStorage(order);
            if (resultObject == null)
            {
                return null;
            }
            var result = new OrderDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public OrderDto EditOrder(OrderDto order)
        {
            var domainModel = _mapper.Map<Order>(order);
            var resultObject = _orderRepository
                .UpdateOrderInStorage(domainModel);
            if (resultObject == null)
                return null;
            var result = new OrderDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public OrderDto GetOrder(Guid? ID)
        {
            Order resultObject = _orderRepository.GetOrderFromStorage(ID);
            if (resultObject == null)
                return null;

            var result = new OrderDto();
            var resultTmp = _mapper.Map(resultObject, result);
            return resultTmp;
        }
    }
}
