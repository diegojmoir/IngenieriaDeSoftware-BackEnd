using AutoMapper;
using Firebase.Database;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services.Injections;
using System.Collections.Generic;
using System.Linq;

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
            var resultObject = _orderRepository
                .UpdateOrderInStorage(order);
            if (resultObject == null)
                return null;
            var result = new OrderDto();
            result = _mapper.Map(resultObject, result);
            return result;
        }

        public bool DeleteOrder(Order order)
        {
            return _orderRepository.DeleteOrderInStorage(order);
        }

        public IEnumerable<OrderDto> GetOrdersByStatus(string status)
        {
            IEnumerable<FirebaseObject<Order>> resultObjects = _orderRepository.GetOrdersFromStorageByStatus(status);
            if (resultObjects == null)
                return null;

            List<OrderDto> resultsObjectList = new List<OrderDto>();
            foreach (FirebaseObject<Order> resultObject in resultObjects)
            {
                var resultTmp = new OrderDto();
                resultTmp = _mapper.Map(resultObject, resultTmp);
                resultsObjectList.Add(resultTmp);
            }
            return resultsObjectList;
        }

        public IEnumerable<OrderDto> GetOrders(string status)
        {
            var resultObjects = _orderRepository.GetOrdersFromStorage();
            return MapOrdersDto(resultObjects);
        }

        private IEnumerable<OrderDto> MapOrdersDto(IEnumerable<Order> _orders)
        {
            var resultsObjectList = _orders.ToList().Select(x =>
            {
                var resultTmp = new OrderDto();
                return _mapper.Map(x, resultTmp);
            });
            return resultsObjectList;
        }
    }
}
