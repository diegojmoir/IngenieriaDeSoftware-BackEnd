using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
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
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product")]
        public IActionResult GetProduct(string id)
        {
            return Ok(id);
            var responseObject = _productService.GetProduct(id);
            if (responseObject == null)
                return NotFound();
            return Ok(responseObject);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            return Ok();
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