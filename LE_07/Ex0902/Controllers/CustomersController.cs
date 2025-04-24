using Ex0902.Data.DTOs;
using Ex0902.Data.Interfaces;
using Ex0902.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ex0902.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly JwtService _jwtService;
        public CustomersController(ICustomerRepository customerRepository, JwtService jwtService)
        {
            _customerRepository = customerRepository;
            _jwtService = jwtService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _customerRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _customerRepository.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _jwtService.GetUserIdFromToken(HttpContext);
            if (userId == null)
                return Unauthorized();

            var newCustomerId = await _customerRepository.CreateCustomerAsync(dto, (int)userId);

            return CreatedAtAction(nameof(GetById), new { id = newCustomerId }, new { id = newCustomerId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            
            var existingCustomer = await _customerRepository.GetByIdAsync(id); if (existingCustomer == null) return NotFound();

            existingCustomer.FirstName = dto.FirstName;
            existingCustomer.LastName = dto.LastName;
            existingCustomer.Street = dto.Street;
            existingCustomer.HouseNo = dto.HouseNo;
            existingCustomer.PostCode = dto.PostCode;
            existingCustomer.City = dto.City;
            existingCustomer.Email = dto.Email;

            if (!ModelState.IsValid) return BadRequest(ModelState);

            
            var updated = await _customerRepository.UpdateCustomerAsync(existingCustomer);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerRepository.DeleteCustomerAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
