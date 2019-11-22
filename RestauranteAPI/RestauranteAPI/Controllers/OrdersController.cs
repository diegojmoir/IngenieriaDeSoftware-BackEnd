using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Mapping;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] OrderDto order)
        {
            if (order == null)
            {
                var errores = new List<string>
                {
                    "La orden no puede ser nula"
                };
                return BadRequest(new
                {
                    errors = errores

                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = (ModelState.Values // TO DO: It should have a custom error message
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }
            if (!order.HasValidDate())
            {
                var errores = new List<string>
                {
                    "Fecha ingresada no válida"
                };
                return BadRequest(new
                {
                    errors = errores

                });
            }

            var responseObject = _orderService.EditOrder(order);

            if (responseObject == null)
            {
                var errores = new List<string>
                {
                    "La orden a editar no existe"
                };
                return BadRequest(new
                {
                    errors = errores

                }); // TO DO: It should have a custom error message
            }

            return Ok(responseObject);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = (ModelState.Values // TO DO: It should have a custom error message
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }

            if (!order.HasValidDate())
            {
                var errores = new List<string>
                {
                    "Fecha(s) ingresadas no válidas"
                };
                return BadRequest(new
                {
                    errors = errores

                });
            }

            var responseObject = _orderService.CreateOrder(order);
            if (responseObject == null)
            {
                return BadRequest(); // TO DO: It should have a custom error message
            }

            return Ok(responseObject);
        }

      
        [HttpGet]
        [Route("getOrder")]
        public IActionResult GetOrder(Guid? ID)
        {
            var responseObject = _orderService.GetOrder(ID);
            if (responseObject == null)
            {
                return NotFound();
            }
            return Ok(responseObject);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete([FromBody] Order order)
        {
            if (order == null)
            {
                var errores = new List<string>
                {
                    "La orden no puede ser nula"
                };
                return BadRequest(new
                {
                    errors = errores

                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = (ModelState.Values // TO DO: It should have a custom error message
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }
            var responseObject = _orderService.DeleteOrder(order);

            if (responseObject == false)
            {
                var errores = new List<string>
                {
                    "La orden a eliminar no existe"
                };
                return BadRequest(new
                {
                    errors = errores

                }); // TO DO: It should have a custom error message
            }
            return Ok("Orden Eliminada");
        }

    }
}