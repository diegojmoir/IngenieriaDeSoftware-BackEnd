using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    /// <summary>
    /// Handles users information
    /// </summary>
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

        /// <summary>
        /// Get user by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(string username, string password)
        {
            var responseObject = _userService.GetUser(username, password);
            if (responseObject == null)
                return NotFound();
            return Ok(responseObject);
        }

        /// <summary>
        /// Create user in schema if provided object is valid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = (ModelState.Values // TO DO: It should have a custom error message
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage))
                });// TO DO: It should have a custom error message
            }

            // Check if username is already taken
            var userSearch = _userService.GetUserByUsername(user.Username);
            if(userSearch != null)
            {
                return BadRequest(); // TODO: Custom message for already taken username
            }

            // Check if email is already taken
            userSearch = _userService.GetUserByEmail(user.Email);
            if (userSearch != null)
            {
                return BadRequest(); // TODO: Custom message for already taken email
            }

            var responseObject = _userService.CreateUser(user);
            if (responseObject == null)
            {
                return BadRequest(); // TO DO: It should have a custom error message
            }
            return Ok(responseObject);            
        }
    }
}