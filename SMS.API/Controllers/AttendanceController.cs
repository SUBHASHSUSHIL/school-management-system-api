using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendances(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var attendances = await _attendanceService.GetAttendanceAsync(pageNumber, pageSize);
                if (attendances == null || !attendances.Any())
                {
                    return NotFound("No attendances found.");
                }
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound($"Attendance with ID {id} not found.");
                }
                return Ok(attendance);
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
        public async Task<IActionResult> CreateAttendance([FromBody] Attendance attendance)
        {
            try
            {
                if (attendance == null)
                {
                    return BadRequest("Attendance data is required.");
                }
                var createdAttendance = await _attendanceService.CreateAttendanceAsync(attendance);
                return CreatedAtAction(nameof(GetAttendances), new { id = createdAttendance.AttendanceId }, createdAttendance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromBody] Attendance attendance)
        {
            try
            {
                if (attendance == null || attendance.AttendanceId <= 0)
                {
                    return BadRequest("Attendance data is invalid.");
                }
                var updatedAttendance = await _attendanceService.UpdateAttendanceAsync(id, attendance);
                if (updatedAttendance == null)
                {
                    return NotFound($"Attendance with ID {id} not found.");
                }
                return Ok(updatedAttendance);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            try
            {
                var result = await _attendanceService.DeleteAttendanceAsync(id);
                if (!result)
                {
                    return NotFound($"Attendance with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
