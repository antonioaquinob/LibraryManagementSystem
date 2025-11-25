using LibraryManagementSystem.API.Services;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/users
        [HttpGet]
        [Authorize] // Only authenticated users can list users
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.RegisterUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _userService.AuthenticateUserAsync(dto);
            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { Token = token });
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUserAsync(id, dto);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
