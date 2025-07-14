using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookCategories(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var bookCategories = await _bookCategoryService.GetBookCategoriesAsync(pageNumber, pageSize);
                if (bookCategories == null || !bookCategories.Any())
                {
                    return Ok("This table is empty.");
                }
                return Ok(bookCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCategoryById(int id)
        {
            try
            {
                var bookCategory = await _bookCategoryService.GetBookCategoryByIdAsync(id);
                if (bookCategory == null)
                {
                    return NotFound($"Book category with ID {id} not found.");
                }
                return Ok(bookCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookCategory([FromBody] CreateBookCategoryDto bookCategory)
        {
            if (bookCategory == null)
            {
                return BadRequest("Book category data is null.");
            }
            try
            {
                var createdBookCategory = await _bookCategoryService.CreateBookCategoryAsync(bookCategory);
                return Ok(createdBookCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookCategory(int id, [FromBody] CreateBookCategoryDto bookCategory)
        {
            if (bookCategory == null)
            {
                return BadRequest("Book category data is null.");
            }
            try
            {
                var updatedBookCategory = await _bookCategoryService.UpdateBookCategoryAsync(id, bookCategory);
                if (updatedBookCategory == null)
                {
                    return NotFound($"Book category with ID {id} not found.");
                }
                return Ok(updatedBookCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCategory(int id)
        {
            try
            {
                var isDeleted = await _bookCategoryService.DeleteBookCategoryAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Book category with ID {id} not found.");
                }
                return Ok($"Book category with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
