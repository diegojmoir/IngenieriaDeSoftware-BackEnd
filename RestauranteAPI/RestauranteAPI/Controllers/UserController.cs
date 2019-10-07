using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        /*
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Credential credential)
        {
            var user = _taxPortalRepository.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        */

        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(string username, string password) 
        {
            var responseObject = _userService.GetUser(username, password);
            if (responseObject == null)
                return NotFound();
            return Ok(responseObject);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(User user)
        {
            var responseObject = _userService.CreateUser(user);
            return Ok(responseObject);
        }

     
    }
}