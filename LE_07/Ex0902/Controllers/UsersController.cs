using Microsoft.AspNetCore.Mvc;
using Ex0902.Models;

namespace Ex0902.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(CreateUser), user);
        }
    }
}
