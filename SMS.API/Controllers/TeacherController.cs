using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var teachers = await _teacherService.GetAllTeachersAsync(pageNumber, pageSize);
                if (teachers == null || !teachers.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                if (teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherDto createTeacher)
        {
            if (createTeacher == null)
            {
                return BadRequest("Invalid teacher data.");
            }
            try
            {
                var createdTeacher = await _teacherService.CreateTeacherAsync(createTeacher);
                return Ok(createdTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] UpdateTeacherDto updateTeacher)
        {
            if (updateTeacher == null)
            {
                return BadRequest("Invalid teacher data.");
            }
            try
            {
                var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, updateTeacher);
                if (updatedTeacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }
                return Ok(updatedTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var result = await _teacherService.DeleteTeacherAsync(id);
                if (!result)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }
                return Ok($"Teacher with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
