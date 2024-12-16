using Microsoft.AspNetCore.Mvc;
using WebDev.BLL.DTO;
using WebDev.BLL.Services;

namespace WebDev.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
        (
            UserService service
        )
        : ControllerBase
    {
        private readonly UserService _userService = service;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            return Ok(await _userService.Login(model));
        }

        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            await _userService.Register(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return Ok(new { Message = "User deleted successfully." });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO model)
        {
            await _userService.UpdateUser(id, model);

            return Ok();
        }

    }
}
