using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly INoticeService _noticeService;

        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotices(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var notices = await _noticeService.GetAllNoticesAsync(pageNumber, pageSize);
                if (notices == null || !notices.Any())
                {
                    return Ok("this table is empty");
                }
                return Ok(notices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticeById(int id)
        {
            try
            {
                var notice = await _noticeService.GetNoticeByIdAsync(id);
                if (notice == null)
                {
                    return NotFound($"Notice with ID {id} not found.");
                }
                return Ok(notice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotice([FromBody] CreateNoticeDto createNotice)
        {
            if (createNotice == null)
            {
                return BadRequest("CreateNoticeDto cannot be null.");
            }
            try
            {
                var createdNotice = await _noticeService.CreateNoticeAsync(createNotice);
                return Ok(createdNotice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotice(int id, [FromBody] UpdateNoticeDto updateNotice)
        {
            if (updateNotice == null || updateNotice.NoticeId != id)
            {
                return BadRequest("UpdateNoticeDto cannot be null and must match the ID in the URL.");
            }
            try
            {
                var updatedNotice = await _noticeService.UpdateNoticeAsync(id, updateNotice);
                return Ok(updatedNotice);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotice(int id)
        {
            try
            {
                var result = await _noticeService.DeleteNoticeAsync(id);
                if (!result)
                {
                    return NotFound($"Notice with ID {id} not found.");
                }
                return Ok($"Notice with ID {id} deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
