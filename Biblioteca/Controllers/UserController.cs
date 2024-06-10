using Biblioteca.Models.Dto;
using Biblioteca.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> saveUser([FromBody] UserDto userDto)
        {
            try
            {
                var newUser = await _userService.saveUser(userDto);
                if (newUser == false) return StatusCode(StatusCodes.Status500InternalServerError, new { success = false });

                return StatusCode(StatusCodes.Status200OK, new { success = true });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
