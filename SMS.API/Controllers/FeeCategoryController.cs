using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCategoryController : ControllerBase
    {
        private readonly IFeeCategoryService _feeCategoryService;

        public FeeCategoryController(IFeeCategoryService feeCategoryService)
        {
            _feeCategoryService = feeCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var feeCategories = await _feeCategoryService.GetAllAsync(pageNumber, pageSize);
                if (feeCategories == null || !feeCategories.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(feeCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var feeCategory = await _feeCategoryService.GetByIdAsync(id);
                if (feeCategory == null)
                {
                    return NotFound($"FeeCategory with ID {id} not found.");
                }
                return Ok(feeCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeeCategoryDto feeCategoryDto)
        {
            if (feeCategoryDto == null)
            {
                return BadRequest("FeeCategory data is null.");
            }
            try
            {
                var createdFeeCategory = await _feeCategoryService.CreateAsync(feeCategoryDto);
                return Ok(createdFeeCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeeCategoryDto feeCategoryDto)
        {
            if (feeCategoryDto == null)
            {
                return BadRequest("FeeCategory data is null.");
            }
            try
            {
                var updatedFeeCategory = await _feeCategoryService.UpdateAsync(id, feeCategoryDto);
                if (updatedFeeCategory == null)
                {
                    return NotFound($"FeeCategory with ID {id} not found.");
                }
                return Ok(updatedFeeCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _feeCategoryService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"FeeCategory with ID {id} not found.");
                }
                return Ok($"FeeCategory with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
