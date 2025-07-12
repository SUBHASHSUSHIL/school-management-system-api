using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EventService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateEventDto> CreateEventAsync(CreateEventDto createEventDto)
        {
            var newEvent = new Event
            {
                Title = createEventDto.Title,
                Description = createEventDto.Description,
                StartDate = createEventDto.StartDate,
                EndDate = createEventDto.EndDate,
                StartTime = createEventDto.StartTime,
                EndTime = createEventDto.EndTime,
                Location = createEventDto.Location,
                Organizer = createEventDto.Organizer,
                CreatedBy = createEventDto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };
            _applicationDbContext.Events.Add(newEvent);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateEventDto
            {
                Title = newEvent.Title,
                Description = newEvent.Description,
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate,
                StartTime = newEvent.StartTime,
                EndTime = newEvent.EndTime,
                Location = newEvent.Location,
                Organizer = newEvent.Organizer,
                CreatedBy = newEvent.CreatedBy,
                CreatedAt = newEvent.CreatedAt
            };
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _applicationDbContext.Events.FindAsync(id);
            if (eventEntity == null)
            {
                return false;
            }
            _applicationDbContext.Events.Remove(eventEntity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<EventDto>> GetAllEventAsync(int pageNumber, int pageSize)
        {
            var events = await _applicationDbContext.Events.OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    Organizer = e.Organizer,
                    CreatedBy = e.CreatedBy,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();
            return events;
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventEntity = await _applicationDbContext.Events
                .Where(e => e.EventId == id)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    Organizer = e.Organizer,
                    CreatedBy = e.CreatedBy,
                    CreatedAt = e.CreatedAt
                })
                .FirstOrDefaultAsync();

            return eventEntity;
        }

        public async Task<CreateEventDto> UpdateEventAsync(int id, CreateEventDto updateEventDto)
        {
            var getEvent = await _applicationDbContext.Events.FindAsync(id);
            if (getEvent == null)
            {
                throw new KeyNotFoundException($"Event with Id {id} not found.");
            }
            getEvent.Title = updateEventDto.Title;
            getEvent.Description = updateEventDto.Description;
            getEvent.StartDate = updateEventDto.StartDate;
            getEvent.EndDate = updateEventDto.EndDate;
            getEvent.StartTime = updateEventDto.StartTime;
            getEvent.EndTime = updateEventDto.EndTime;
            getEvent.Location = updateEventDto.Location;
            getEvent.Organizer = updateEventDto.Organizer;
            getEvent.CreatedBy = updateEventDto.CreatedBy;
            getEvent.CreatedAt = updateEventDto.CreatedAt;
            _applicationDbContext.Events.Update(getEvent);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateEventDto
            {
                Title = getEvent.Title,
                Description = getEvent.Description,
                StartDate = getEvent.StartDate,
                EndDate = getEvent.EndDate,
                StartTime = getEvent.StartTime,
                EndTime = getEvent.EndTime,
                Location = getEvent.Location,
                Organizer = getEvent.Organizer,
                CreatedBy = getEvent.CreatedBy,
                CreatedAt = getEvent.CreatedAt
            };
        }
    }
}
