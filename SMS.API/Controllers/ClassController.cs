using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClasses(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var classes = await _classService.GetClassesAsync(pageNumber, pageSize);
                if (classes == null || !classes.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{classId}")]
        public async Task<IActionResult> GetClassById(int classId)
        {
            try
            {
                var classDto = await _classService.GetClassByIdAsync(classId);
                if (classDto == null)
                {
                    return NotFound($"Class with ID {classId} not found.");
                }
                return Ok(classDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            if (createClassDto == null)
            {
                return BadRequest("Class data is null.");
            }
            try
            {
                var createdClass = await _classService.CreateClassAsync(createClassDto);
                return Ok(createdClass);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{classId}")]
        public async Task<IActionResult> UpdateClass(int classId, [FromBody] CreateClassDto updateClassDto)
        {
            if (updateClassDto == null)
            {
                return BadRequest("Class data is null.");
            }
            try
            {
                var result = await _classService.UpdateClassAsync(classId, updateClassDto);
                if (!result)
                {
                    return NotFound($"Class with ID {classId} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{classId}")]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            try
            {
                var result = await _classService.DeleteClassAsync(classId);
                if (!result)
                {
                    return NotFound($"Class with ID {classId} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
