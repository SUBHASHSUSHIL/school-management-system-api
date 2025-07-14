using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var staffs = await _staffService.GetAllStaffAsync(pageNumber, pageSize);
                if (staffs == null || !staffs.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(staffs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            try
            {
                var staff = await _staffService.GetStaffByIdAsync(id);
                if (staff == null)
                {
                    return NotFound($"Staff with ID {id} not found.");
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto createStaff)
        {
            if (createStaff == null)
            {
                return BadRequest("CreateStaffDto cannot be null.");
            }
            try
            {
                var newStaff = await _staffService.CreateStaffAsync(createStaff);
                return Ok(newStaff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffDto updateStaff)
        {
            if (updateStaff == null)
            {
                return BadRequest("UpdateStaffDto cannot be null.");
            }
            try
            {
                var updatedStaff = await _staffService.UpdateStaffAsync(id, updateStaff);
                if (updatedStaff == null)
                {
                    return NotFound($"Staff with ID {id} not found.");
                }
                return Ok(updatedStaff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            try
            {
                var isDeleted = await _staffService.DeleteStaffAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Staff with ID {id} not found.");
                }
                return Ok($"Staff with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
