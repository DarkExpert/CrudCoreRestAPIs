using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDWrox.API.Core;
using SDWrox.API.Services;
using SDWrox.DataModel.SecurityModels;
using SDWrox.Entity;

namespace SDWrox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        // POST api/Users
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] User userObj)
        {
            userObj.Id = 0;
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }
    }
}
