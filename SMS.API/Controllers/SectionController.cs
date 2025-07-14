using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSections(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var sections = await _sectionService.GetAllSectionsAsync(pageNumber, pageSize);
                if (sections == null || !sections.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(sections);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSectionById(int id)
        {
            try
            {
                var section = await _sectionService.GetSectionByIdAsync(id);
                if (section == null)
                {
                    return NotFound($"Section with ID {id} not found.");
                }
                return Ok(section);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionDto createSection)
        {
            if (createSection == null)
            {
                return BadRequest("CreateSectionDto cannot be null.");
            }
            try
            {
                var createdSection = await _sectionService.CreateSectionAsync(createSection);
                return CreatedAtAction(nameof(GetSectionById), new { id = createdSection.SectionId }, createdSection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(int id, [FromBody] UpdateSectionDto updateSection)
        {
            if (updateSection == null || updateSection.SectionId != id)
            {
                return BadRequest("Invalid section data.");
            }
            try
            {
                var updatedSection = await _sectionService.UpdateSectionAsync(id, updateSection);
                if (updatedSection == null)
                {
                    return NotFound($"Section with ID {id} not found.");
                }
                return Ok(updatedSection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            try
            {
                var isDeleted = await _sectionService.DeleteSectionAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Section with ID {id} not found.");
                }
                return Ok($"Section with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
