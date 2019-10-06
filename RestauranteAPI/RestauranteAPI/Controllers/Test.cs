using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test:ControllerBase
    {
        private ITestService _testService;
        public Test(ITestService testService) 
        {
            _testService = testService;
        }
        /// <summary>
        /// Return test string
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("testvalues")]
        public IActionResult GetTestValues() 
        {
            return Ok("Hola como estan");
        }

        [HttpGet]
        [Route("testdata")]

        public IActionResult GetTestDataFromFirebase() 
        {
            return Ok(_testService.GetTestDataFromFireBase());
        }

    
    }
}
