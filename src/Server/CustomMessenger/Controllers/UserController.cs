using CustomMessenger.Service.DTO.Users;
using CustomMessenger.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomMessenger.Controllers
{
    [ApiController, Route("users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsyc(UserForLogin login)
            => Ok(await userService.LoginAsync(login));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserForCreation user)
        {
            await userService.CreateAsync(user);
            return Ok();
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await userService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] string query = null)
            => Ok(await userService.GetAllAsync(query));

        [HttpGet("/self"), Authorize]
        public async Task<IActionResult> GetSelfAsync()
            => Ok(await userService.GetSelfAsync());

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Ok(await userService.GetByIdAsync(id));

        [HttpGet("username/{username}"), Authorize]
        public async Task<IActionResult> GetByUsernameAsync(string username)
            => Ok(await userService.GetByUsernameAsync(username));

        [HttpGet("phone/{phonenumber}"), Authorize]
        public async Task<IActionResult> GetByPhoneNumberAsync(string phonenumber)
            => Ok(await userService.GetByNumberAsync(phonenumber));
    }
}
