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
    public class SectionService : ISectionService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SectionService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateSectionDto> CreateSectionAsync(CreateSectionDto createSection)
        {
            var newSection = new Section
            {
                SectionName = createSection.SectionName,
                ClassId = createSection.ClassId,
                Capacity = createSection.Capacity,
                ClassTeacherId = createSection.ClassTeacherId,
                RoomNumber = createSection.RoomNumber
            };
            await _applicationDbContext.Sections.AddAsync(newSection);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateSectionDto
            {
                SectionId = newSection.SectionId,
                SectionName = newSection.SectionName,
                ClassId = newSection.ClassId,
                Capacity = newSection.Capacity,
                ClassTeacherId = newSection.ClassTeacherId,
                RoomNumber = newSection.RoomNumber
            };
        }

        public async Task<bool> DeleteSectionAsync(int id)
        {
            var section = await _applicationDbContext.Sections.FindAsync(id);
            if (section == null)
            {
                throw new KeyNotFoundException($"Section with ID {id} not found.");
            }
            _applicationDbContext.Sections.Remove(section);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SectionDto>> GetAllSectionsAsync(int pageNumber, int pageSize)
        {
            var sections = await _applicationDbContext.Sections.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SectionDto
                {
                    SectionId = s.SectionId,
                    SectionName = s.SectionName,
                    ClassId = s.ClassId,
                    Capacity = s.Capacity,
                    ClassTeacherId = s.ClassTeacherId,
                    RoomNumber = s.RoomNumber
                }).OrderByDescending(s => s.SectionId).ToListAsync();
            return sections;
        }

        public async Task<SectionDto> GetSectionByIdAsync(int id)
        {
            var section = await _applicationDbContext.Sections
                .Where(s => s.SectionId == id)
                .Select(s => new SectionDto
                {
                    SectionId = s.SectionId,
                    SectionName = s.SectionName,
                    ClassId = s.ClassId,
                    Capacity = s.Capacity,
                    ClassTeacherId = s.ClassTeacherId,
                    RoomNumber = s.RoomNumber
                }).FirstOrDefaultAsync();
            if (section == null)
            {
                throw new KeyNotFoundException($"Section with ID {id} not found.");
            }
            return section;
        }

        public async Task<UpdateSectionDto> UpdateSectionAsync(int id, UpdateSectionDto updateSection)
        {
            var section = await _applicationDbContext.Sections.FindAsync(id);
            if (section == null)
            {
                throw new KeyNotFoundException($"Section with ID {id} not found.");
            }
            section.SectionName = updateSection.SectionName;
            section.ClassId = updateSection.ClassId;
            section.Capacity = updateSection.Capacity;
            section.ClassTeacherId = updateSection.ClassTeacherId;
            section.RoomNumber = updateSection.RoomNumber;
            _applicationDbContext.Sections.Update(section);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateSectionDto
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName,
                ClassId = section.ClassId,
                Capacity = section.Capacity,
                ClassTeacherId = section.ClassTeacherId,
                RoomNumber = section.RoomNumber
            };
        }
    }
}
