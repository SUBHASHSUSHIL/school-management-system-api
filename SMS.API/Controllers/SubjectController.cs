using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var subjects = await _subjectService.GetAllSubjectsAsync(pageNumber, pageSize);
                if (subjects == null || !subjects.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto createSubject)
        {
            try
            {
                if (createSubject == null)
                {
                    return BadRequest("Create subject data is null.");
                }
                var createdSubject = await _subjectService.CreateSubjectAsync(createSubject);
                return Ok(createdSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] UpdateSubjectDto updateSubject)
        {
            try
            {
                if (updateSubject == null)
                {
                    return BadRequest("Update subject data is null.");
                }
                var updatedSubject = await _subjectService.UpdateSubjectAsync(id, updateSubject);
                if (updatedSubject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }
                return Ok(updatedSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var isDeleted = await _subjectService.DeleteSubjectAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }
                return Ok($"Subject with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
