using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
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
        public IActionResult Create([FromBody] User user)
        {
            var responseObject = _userService.CreateUser(user);
            if (user == null)
                return BadRequest();
            return Ok(responseObject);
        }
    }
}