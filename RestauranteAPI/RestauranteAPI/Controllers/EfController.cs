using System;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    public class EfController:ControllerBase
    {
        private readonly IEfTestService _efTestService;

        public EfController(IEfTestService efTestService)
        {
            _efTestService = efTestService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult SetEfTestObject(EfTest someTest)
        {
            someTest.Key = Guid.NewGuid().ToString();
            _efTestService.SaveEfTestService(someTest);
            return Ok();
        }
    }
}
