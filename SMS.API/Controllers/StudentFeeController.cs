using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFeeController : ControllerBase
    {
        private readonly IStudentFeeService _studentFeeService;

        public StudentFeeController(IStudentFeeService studentFeeService)
        {
            _studentFeeService = studentFeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentFees(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var studentFees = await _studentFeeService.GetAllStudentFeesAsync(pageNumber, pageSize);
                if (studentFees == null || !studentFees.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(studentFees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentFeeById(int id)
        {
            try
            {
                var studentFee = await _studentFeeService.GetStudentFeeByIdAsync(id);
                if (studentFee == null)
                {
                    return NotFound($"Student fee with ID {id} not found.");
                }
                return Ok(studentFee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentFee([FromBody] CreateStudentFeeDto createStudentFeeDto)
        {
            try
            {
                if (createStudentFeeDto == null)
                {
                    return BadRequest("Student fee data is required.");
                }
                var createdStudentFee = await _studentFeeService.CreateStudentFeeAsync(createStudentFeeDto);
                return Ok(createdStudentFee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentFee(int id, [FromBody] UpdateStudentFeeDto updateStudentFeeDto)
        {
            try
            {
                if (id <= 0 || updateStudentFeeDto == null)
                {
                    return BadRequest("Invalid student fee ID or data.");
                }
                var updatedStudentFee = await _studentFeeService.UpdateStudentFeeAsync(id, updateStudentFeeDto);
                if (updatedStudentFee == null)
                {
                    return NotFound($"Student fee with ID {id} not found.");
                }
                return Ok(updatedStudentFee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteStudentFee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid student fee ID.");
                }
                await _studentFeeService.DeleteStudentFeeAsync(id);
                return Ok($"Student fee with ID {id} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
