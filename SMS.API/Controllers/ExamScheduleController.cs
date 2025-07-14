using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamScheduleController : ControllerBase
    {
        private readonly IExamScheduleService _examScheduleService;

        public ExamScheduleController(IExamScheduleService examScheduleService)
        {
            _examScheduleService = examScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamSchedules(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var examSchedules = await _examScheduleService.GetAllExamSchedulesAsync(pageNumber, pageSize);
                if (examSchedules == null || !examSchedules.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(examSchedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateExamSchedule([FromBody] CreateExamScheduleDto createExamScheduleDto)
        {
            try
            {
                if (createExamScheduleDto == null)
                {
                    return BadRequest("Invalid exam schedule data.");
                }
                var result = await _examScheduleService.CreateExamScheduleAsync(createExamScheduleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamScheduleById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid exam schedule ID.");
                }
                var examSchedule = await _examScheduleService.GetExamScheduleByIdAsync(id);
                if (examSchedule == null)
                {
                    return NotFound($"Exam schedule with ID {id} not found.");
                }
                return Ok(examSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamSchedule(int id, [FromBody] UpdateExamScheduleDto updateExamScheduleDto)
        {
            try
            {
                if (id <= 0 || updateExamScheduleDto == null)
                {
                    return BadRequest("Invalid exam schedule ID or data.");
                }
                var result = await _examScheduleService.UpdateExamScheduleAsync(id, updateExamScheduleDto);
                if (result == null)
                {
                    return NotFound($"Exam schedule with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamSchedule(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid exam schedule ID.");
                }
                var result = await _examScheduleService.DeleteExamScheduleAsync(id);
                if (!result)
                {
                    return NotFound($"Exam schedule with ID {id} not found.");
                }
                return Ok($"Exam schedule with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
