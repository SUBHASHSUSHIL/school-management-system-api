using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParents(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var parents = await _parentService.GetAllParentsAsync(pageNumber, pageSize);
                if (parents == null || !parents.Any())
                {
                    return Ok("This table is empty");
                }
                return Ok(parents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParentById(int id)
        {
            try
            {
                var parent = await _parentService.GetParentByIdAsync(id);
                if (parent == null)
                {
                    return NotFound($"Parent with ID {id} not found.");
                }
                return Ok(parent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] CreateParentDto createParent)
        {
            if (createParent == null)
            {
                return BadRequest("Parent data is null.");
            }
            try
            {
                var createdParent = await _parentService.CreateParentAsync(createParent);
                return Ok(createdParent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParent(int id, [FromBody] UpdateParentDto updateParent)
        {
            if (updateParent == null)
            {
                return BadRequest("Parent data is null.");
            }
            if (id != updateParent.ParentId)
            {
                return BadRequest("Parent ID mismatch.");
            }
            try
            {
                var updatedParent = await _parentService.UpdateParentAsync(id, updateParent);
                if (updatedParent == null)
                {
                    return NotFound($"Parent with ID {id} not found.");
                }
                return Ok(updatedParent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            try
            {
                var isDeleted = await _parentService.DeleteParentAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Parent with ID {id} not found.");
                }
                return Ok($"Parent with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
