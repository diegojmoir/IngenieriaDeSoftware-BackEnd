using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Dto;
using RestauranteAPI.Services.Injections;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    /// <summary>
    /// Handles products information
    /// </summary>
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
       
        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] ProductDto product)
        {
            
            if (product == null)
            {
                var errores = new List<string>
                {
                    "El producto no puede ser nulo"
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
            if (!product.HasValidDate())
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

            var responseObject = _productService.EditProduct(product);

            if (responseObject == null)
            {
                var errores = new List<string>
                {
                    "El producto a editar no existe"
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
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
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

            if (!product.HasValidDate())
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

            var responseObject = _productService.CreateProduct(product);
            if (responseObject == null)
            {
                return BadRequest(); // TO DO: It should have a custom error message
            }

            return Ok(responseObject);
        }

        [HttpGet]
        [Route("getAvailable")]
        public IActionResult GetAvailable()
        {
            var responseObject = _productService.GetAvailable();
            if(responseObject == null)
            {
                return NotFound();
            }
            return Ok(responseObject);
        }
    }   
}