using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssueController : ControllerBase
    {
        private readonly IBookIssueService _bookIssueService;

        public BookIssueController(IBookIssueService bookIssueService)
        {
            _bookIssueService = bookIssueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookIssues(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var bookIssues = await _bookIssueService.GetBookIssueAsync(pageNumber, pageSize);
                if (bookIssues == null || !bookIssues.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(bookIssues);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving book issues: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookIssueById(int id)
        {
            try
            {
                var bookIssue = await _bookIssueService.GetByIdAsync(id);
                if (bookIssue == null)
                {
                    return NotFound($"Book issue with ID {id} not found.");
                }
                return Ok(bookIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving book issue: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookIssue([FromBody] CreateBookIssueDto bookIssueDto)
        {
            if (bookIssueDto == null)
            {
                return BadRequest("Book issue data is null.");
            }
            try
            {
                var createdBookIssue = await _bookIssueService.CreateBookAsync(bookIssueDto);
                return CreatedAtAction(nameof(GetBookIssueById), new { id = createdBookIssue.BookId }, createdBookIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating book issue: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookIssue(int id, [FromBody] CreateBookIssueDto bookIssueDto)
        {
            if (bookIssueDto == null)
            {
                return BadRequest("Book issue data is null.");
            }
            try
            {
                var updatedBookIssue = await _bookIssueService.UpdateBookAsync(id, bookIssueDto);
                if (updatedBookIssue == null)
                {
                    return NotFound($"Book issue with ID {id} not found.");
                }
                return Ok(updatedBookIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating book issue: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookIssue(int id)
        {
            try
            {
                var isDeleted = await _bookIssueService.DeleteBookAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Book issue with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting book issue: {ex.Message}");
            }
        }
    }
}
