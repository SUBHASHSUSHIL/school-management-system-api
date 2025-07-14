using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHomeworkController : ControllerBase
    {
        private readonly IStudentHomeworkService _studentHomeworkService;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkService)
        {
            _studentHomeworkService = studentHomeworkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentHomeworks(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var studentHomeworks = await _studentHomeworkService.GetAllStudentHomeworksAsync(pageNumber, pageSize);
                if (studentHomeworks == null || !studentHomeworks.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(studentHomeworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentHomeworkById(int id)
        {
            try
            {
                var studentHomework = await _studentHomeworkService.GetStudentHomeworkByIdAsync(id);
                if (studentHomework == null)
                {
                    return NotFound($"Student homework with ID {id} not found.");
                }
                return Ok(studentHomework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentHomework([FromBody] CreateStudentHomeworkDto createStudentHomework)
        {
            if (createStudentHomework == null)
            {
                return BadRequest("Invalid student homework data.");
            }
            try
            {
                var createdHomework = await _studentHomeworkService.CreateStudentHomeworkAsync(createStudentHomework);
                return CreatedAtAction(nameof(GetStudentHomeworkById), new { id = createdHomework.StudentHomeworkId }, createdHomework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentHomework(int id, [FromBody] UpdateStudentHomeworkDto updateStudentHomework)
        {
            if (updateStudentHomework == null || id != updateStudentHomework.StudentHomeworkId)
            {
                return BadRequest("Invalid student homework data.");
            }
            try
            {
                var updatedHomework = await _studentHomeworkService.UpdateStudentHomeworkAsync(id, updateStudentHomework);
                if (updatedHomework == null)
                {
                    return NotFound($"Student homework with ID {id} not found.");
                }
                return Ok(updatedHomework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentHomework(int id)
        {
            try
            {
                var isDeleted = await _studentHomeworkService.DeleteStudentHomeworkAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Student homework with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
