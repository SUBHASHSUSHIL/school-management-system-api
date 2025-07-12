using SMS.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllEventAsync(int pageNumber, int pageSize);
        Task<EventDto> GetEventByIdAsync(int id);
        Task<CreateEventDto> CreateEventAsync(CreateEventDto createEventDto);
        Task<CreateEventDto> UpdateEventAsync(int id, CreateEventDto updateEventDto);
        Task<bool> DeleteEventAsync(int id);
    }
}
