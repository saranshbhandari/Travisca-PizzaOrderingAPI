using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaOrderingSystem.Ordering.API.Authentication;
namespace PizzaOrderingSystem.Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtUtility _jwtUtility;

        public UserController(IUserService userService, IJwtUtility jwtUtility)
        {
            _userService = userService;
            _jwtUtility=jwtUtility;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            //var response = await _jwtUtility.Authenticate(model);
            User user = await _userService.GetFromNameAsync(model.Username);
            if (user == null || user.Password != model.Password)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var token=_jwtUtility.GenerateToken(user);
            AuthenticateResponse response = new(user, token);
           
            return Ok(response);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAsync();
            return Ok(users);
        }
    }
}
