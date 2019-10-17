using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;
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
    }
}