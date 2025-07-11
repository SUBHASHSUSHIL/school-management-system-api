using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAcademicYears(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var academicYears = await _academicYearService.GetAllAcademicYearsAsync(pageNumber, pageSize);
                if (academicYears == null || !academicYears.Any())
                {
                    return NotFound("No academic years found.");
                }
                return Ok(academicYears);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{academicYearId}")]
        public async Task<IActionResult> GetAcademicYearById(int academicYearId)
        {
            try
            {
                var academicYear = await _academicYearService.GetAcademicYearByIdAsync(academicYearId);
                if (academicYear == null)
                {
                    return NotFound($"Academic Year with ID {academicYearId} not found.");
                }
                return Ok(academicYear);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAcademicYear([FromBody] AcademicYear academicYear)
        {
            if (academicYear == null)
            {
                return BadRequest("Academic Year data is null.");
            }
            try
            {
                var createdAcademicYear = await _academicYearService.CreateAcademicYearAsync(academicYear);
                return CreatedAtAction(nameof(GetAcademicYearById), new { academicYearId = createdAcademicYear.AcademicYearId }, createdAcademicYear);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{academicYearId}")]
        public async Task<IActionResult> UpdateAcademicYear(int academicYearId, [FromBody] AcademicYear academicYear)
        {
            if (academicYear == null)
            {
                return BadRequest("Academic Year data is null.");
            }
            try
            {
                var updatedAcademicYear = await _academicYearService.UpdateAcademicYearAsync(academicYearId, academicYear);
                if (updatedAcademicYear == null)
                {
                    return NotFound($"Academic Year with ID {academicYearId} not found.");
                }
                return Ok(updatedAcademicYear);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{academicYearId}")]
        public async Task<IActionResult> DeleteAcademicYear(int academicYearId)
        {
            try
            {
                var isDeleted = await _academicYearService.DeleteAcademicYearAsync(academicYearId);
                if (!isDeleted)
                {
                    return NotFound($"Academic Year with ID {academicYearId} not found.");
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
