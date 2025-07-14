using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var exams = await _examService.GetAllExamAsync(pageNumber, pageSize);
                if (exams == null || !exams.Any())
                {
                    return Ok("This table is empty");
                }
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(int id)
        {
            try
            {
                var exam = await _examService.GetExamByIdAsync(id);
                if (exam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDto createExamDto)
        {
            if (createExamDto == null)
            {
                return BadRequest("Exam data is null.");
            }
            try
            {
                var createdExam = await _examService.CreateExamAsync(createExamDto);
                return Ok(createdExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] CreateExamDto updateExamDto)
        {
            if (updateExamDto == null)
            {
                return BadRequest("Exam data is null.");
            }
            try
            {
                var updatedExam = await _examService.UpdateExamAsync(id, updateExamDto);
                if (updatedExam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }
                return Ok(updatedExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                var result = await _examService.DeleteExamAsync(id);
                if (!result)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }
                return Ok($"Exam with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
