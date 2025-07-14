using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimetables(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var timetables = await _timetableService.GetAllTimetablesAsync(pageNumber, pageSize);
                if (timetables == null || !timetables.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(timetables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimetableById(int id)
        {
            try
            {
                var timetable = await _timetableService.GetTimetableByIdAsync(id);
                if (timetable == null)
                {
                    return NotFound($"Timetable with ID {id} not found.");
                }
                return Ok(timetable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimetable([FromBody] CreateTimetableDto createTimetable)
        {
            if (createTimetable == null)
            {
                return BadRequest("Timetable data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdTimetable = await _timetableService.CreateTimetableAsync(createTimetable);
                return Ok(createdTimetable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimetable(int id, [FromBody] UpdateTimetableDto updateTimetable)
        {
            if (updateTimetable == null)
            {
                return BadRequest("Timetable data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedTimetable = await _timetableService.UpdateTimetableAsync(id, updateTimetable);
                if (updatedTimetable == null)
                {
                    return NotFound($"Timetable with ID {id} not found.");
                }
                return Ok(updatedTimetable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetable(int id)
        {
            try
            {
                var result = await _timetableService.DeleteTimetableAsync(id);
                if (!result)
                {
                    return NotFound($"Timetable with ID {id} not found.");
                }
                return Ok($"Timetable with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
