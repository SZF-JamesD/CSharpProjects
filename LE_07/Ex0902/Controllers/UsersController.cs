using Ex0902.Data.DTOs;
using Ex0902.Data.Interfaces;
using Ex0902.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ex0902.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public UsersController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
           


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newUserId = await _userRepository.CreateUserAsync(dto);

            if (newUserId == null) return Conflict("Username is already taken.");

            return CreatedAtAction(null, new { id = newUserId});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Username and password are required.");

            var uid = await _userRepository.AuthenticateAsync(userDto.Username, userDto.Password);
            if (!uid.HasValue) return Unauthorized();

            var token = _jwtService.GenerateToken(uid.Value);
            return Ok(new { token });
        }
    }
}
