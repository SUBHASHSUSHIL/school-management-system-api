using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeStructureController : ControllerBase
    {
        private readonly IFeeStructureService _feeStructureService;

        public FeeStructureController(IFeeStructureService feeStructureService)
        {
            _feeStructureService = feeStructureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeeStructures(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var feeStructures = await _feeStructureService.GetAllFeeStructuresAsync(pageNumber, pageSize);
                if (feeStructures == null || !feeStructures.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(feeStructures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeeStructureById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID.");
                }
                var feeStructure = await _feeStructureService.GetFeeStructureByIdAsync(id);
                if (feeStructure == null)
                {
                    return NotFound($"Fee structure with ID {id} not found.");
                }
                return Ok(feeStructure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeeStructure([FromBody] FeeStructureCreateDto createFeeStructureDto)
        {
            try
            {
                if (createFeeStructureDto == null)
                {
                    return BadRequest("Fee structure data is null.");
                }
                var createdFeeStructure = await _feeStructureService.CreateFeeStructureAsync(createFeeStructureDto);
                return Ok(createdFeeStructure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeeStructure(int id, [FromBody] FeeStructureUpdateDto updateFeeStructureDto)
        {
            try
            {
                if (id <= 0 || updateFeeStructureDto == null)
                {
                    return BadRequest("Invalid ID or data.");
                }
                var updatedFeeStructure = await _feeStructureService.UpdateFeeStructureAsync(id, updateFeeStructureDto);
                if (updatedFeeStructure == null)
                {
                    return NotFound($"Fee structure with ID {id} not found.");
                }
                return Ok(updatedFeeStructure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeStructure(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID.");
                }
                var isDeleted = await _feeStructureService.DeleteFeeStructureAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Fee structure with ID {id} not found.");
                }
                return Ok($"Fee structure with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
