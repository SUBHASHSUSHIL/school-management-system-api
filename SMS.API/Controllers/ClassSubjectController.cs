using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSubjectController : ControllerBase
    {
        private readonly IClassSubjectService _classSubjectService;

        public ClassSubjectController(IClassSubjectService classSubjectService)
        {
            _classSubjectService = classSubjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassSubjects(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var classSubjects = await _classSubjectService.GetClassSubjectsAsync(pageNumber, pageSize);
                if (classSubjects == null || !classSubjects.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(classSubjects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassSubjectById(int id)
        {
            try
            {
                var classSubject = await _classSubjectService.GetClassSubjectByIdAsync(id);
                if (classSubject == null)
                {
                    return NotFound($"Class subject with ID {id} not found.");
                }
                return Ok(classSubject);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassSubject([FromBody] CreateClassSubjectDto classSubject)
        {
            if (classSubject == null)
            {
                return BadRequest("Class subject data is null.");
            }
            try
            {
                var createdClassSubject = await _classSubjectService.CreateClassSubjectAsync(classSubject);
                return Ok(createdClassSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassSubject(int id, [FromBody] CreateClassSubjectDto classSubject)
        {
            if (classSubject == null)
            {
                return BadRequest("Class subject data is null.");
            }
            try
            {
                var updatedClassSubject = await _classSubjectService.UpdateClassSubjectAsync(id, classSubject);
                if (updatedClassSubject == null)
                {
                    return NotFound($"Class subject with ID {id} not found.");
                }
                return Ok(updatedClassSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassSubject(int id)
        {
            try
            {
                var isDeleted = await _classSubjectService.DeleteClassSubjectAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Class subject with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
