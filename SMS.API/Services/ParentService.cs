using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.DTOs;
using SMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class ParentService : IParentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ParentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateParentDto> CreateParentAsync(CreateParentDto createParent)
        {
            var newParent = new Domain.Models.Parent
            {
                UserId = createParent.UserId,
                Occupation = createParent.Occupation,
                AnnualIncome = createParent.AnnualIncome,
                Education = createParent.Education
            };
            _applicationDbContext.Parents.Add(newParent);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateParentDto
            {
                ParentId = newParent.ParentId,
                UserId = newParent.UserId,
                Occupation = newParent.Occupation,
                AnnualIncome = newParent.AnnualIncome,
                Education = newParent.Education
            };
        }

        public Task<bool> DeleteParentAsync(int id)
        {
            var parent = _applicationDbContext.Parents.Find(id);
            if (parent == null)
            {
                throw new KeyNotFoundException($"Parent with ID {id} not found.");
            }
            _applicationDbContext.Parents.Remove(parent);
            return _applicationDbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<ParentDto>> GetAllParentsAsync(int pageNumber, int pageSize)
        {
            var parents = await _applicationDbContext.Parents
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ParentDto
                {
                    ParentId = p.ParentId,
                    UserId = p.UserId,
                    Occupation = p.Occupation,
                    AnnualIncome = p.AnnualIncome,
                    Education = p.Education
                }).OrderByDescending(x => x.ParentId).ToListAsync();
            return parents;
        }

        public async Task<ParentDto> GetParentByIdAsync(int id)
        {
            var parent = await _applicationDbContext.Parents
                .Where(p => p.ParentId == id)
                .Select(p => new ParentDto
                {
                    ParentId = p.ParentId,
                    UserId = p.UserId,
                    Occupation = p.Occupation,
                    AnnualIncome = p.AnnualIncome,
                    Education = p.Education
                }).FirstOrDefaultAsync();
            if (parent == null)
            {
                throw new KeyNotFoundException($"Parent with ID {id} not found.");
            }
            return parent;
        }

        public async Task<UpdateParentDto> UpdateParentAsync(int id, UpdateParentDto updateParent)
        {
            var parent = await _applicationDbContext.Parents.FindAsync(id);
            if (parent == null)
            {
                throw new KeyNotFoundException($"Parent with ID {id} not found.");
            }
            parent.ParentId = updateParent.ParentId;
            parent.UserId = updateParent.UserId;
            parent.Occupation = updateParent.Occupation;
            parent.AnnualIncome = updateParent.AnnualIncome;
            parent.Education = updateParent.Education;
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateParentDto
            {
                ParentId = parent.ParentId,
                UserId = parent.UserId,
                Occupation = parent.Occupation,
                AnnualIncome = parent.AnnualIncome,
                Education = parent.Education
            };
        }
    }
}
