using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        private readonly IExamResultService _examResultService;

        public ExamResultController(IExamResultService examResultService)
        {
            _examResultService = examResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamResults(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var results = await _examResultService.GetAllExamResultsAsync(pageNumber, pageSize);
                if (results == null || !results.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving exam results: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamResultById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid exam result ID.");
                }
                var result = await _examResultService.GetExamResultByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Exam result with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving exam result: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamResult([FromBody] ExamResultCreateDto examResultCreateDto)
        {
            try
            {
                if (examResultCreateDto == null)
                {
                    return BadRequest("Exam result data is required.");
                }
                var createdResult = await _examResultService.CreateExamResultAsync(examResultCreateDto);
                return Ok(createdResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating exam result: {ex.Message}");
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateExamResult(int id, [FromBody] ExamResultUpdateDto examResultUpdateDto)
        {
            try
            {
                if (id <= 0 || examResultUpdateDto == null)
                {
                    return BadRequest("Invalid exam result ID or data.");
                }
                var updatedResult = await _examResultService.UpdateExamResultAsync(id, examResultUpdateDto);
                if (updatedResult == null)
                {
                    return NotFound($"Exam result with ID {id} not found.");
                }
                return Ok(updatedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating exam result: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamResult(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid exam result ID.");
                }
                var isDeleted = await _examResultService.DeleteExamResultAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Exam result with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting exam result: {ex.Message}");
            }
        }
    }
}
