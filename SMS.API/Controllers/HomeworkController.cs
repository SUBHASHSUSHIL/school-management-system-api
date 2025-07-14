using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;

        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHomeworks(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var homeworks = await _homeworkService.GetAllHomeworksAsync(pageNumber, pageSize);
                if (homeworks == null || !homeworks.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(homeworks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeworkById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid homework ID.");
                }
                var homework = await _homeworkService.GetHomeworkByIdAsync(id);
                if (homework == null)
                {
                    return NotFound($"Homework with ID {id} not found.");
                }
                return Ok(homework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomework([FromBody] CreateHomeworkDto homeworkDto)
        {
            try
            {
                if (homeworkDto == null)
                {
                    return BadRequest("Homework data is required.");
                }
                var createdHomework = await _homeworkService.CreateHomeworkAsync(homeworkDto);
                return Ok(createdHomework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHomework(int id, [FromBody] UpdateHomeworkDto homeworkDto)
        {
            try
            {
                if (id <= 0 || homeworkDto == null)
                {
                    return BadRequest("Invalid homework ID or data.");
                }
                var updatedHomework = await _homeworkService.UpdateHomeworkAsync(id, homeworkDto);
                if (updatedHomework == null)
                {
                    return NotFound($"Homework with ID {id} not found.");
                }
                return Ok(updatedHomework);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid homework ID.");
                }
                var result = await _homeworkService.DeleteHomeworkAsync(id);
                if (!result)
                {
                    return NotFound($"Homework with ID {id} not found.");
                }
                return Ok($"Homework with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
