using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var getEvents = await _eventService.GetAllEventAsync(pageNumber, pageSize);
                if (getEvents == null)
                {
                    return Ok("This table is empty.");
                }
                return Ok(getEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById (int id)
        {
            try
            {
                var getEvent = await _eventService.GetEventByIdAsync(id);
                if (getEvent == null)
                {
                    return NotFound($"Event with Id {id} not found.");
                }
                return Ok(getEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent (CreateEventDto createEvent)
        {
            try
            {
                if (createEvent == null)
                {
                    return BadRequest("Event data is required");
                }
                var addEvent = await _eventService.CreateEventAsync(createEvent);
                return Ok(addEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateEvent (int id, [FromBody] CreateEventDto updateEvent)
        {
            try
            {
                if (updateEvent == null || id <= 0)
                {
                    return BadRequest("Valid event data is required.");
                }
                var updateEvt = await _eventService.UpdateEventAsync(id, updateEvent);
                if (updateEvt == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(updateEvt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEvent (int id)
        {
            try
            {
                var isDeleted = await _eventService.DeleteEventAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
