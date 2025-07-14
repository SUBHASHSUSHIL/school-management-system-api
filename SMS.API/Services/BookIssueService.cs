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
    public class BookIssueService : IBookIssueService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookIssueService(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<CreateBookIssueDto> CreateBookAsync(CreateBookIssueDto bookIssueDto)
        {
            var newBookIssue = new BookIssue
            {
                BookId = bookIssueDto.BookId,
                UserId = bookIssueDto.UserId,
                IssueDate = bookIssueDto.IssueDate,
                DueDate = bookIssueDto.DueDate,
                ReturnDate = bookIssueDto.ReturnDate,
                Status = bookIssueDto.Status,
                FineAmount = bookIssueDto.FineAmount,
                Remarks = bookIssueDto.Remarks,
                IssuedBy = bookIssueDto.IssuedBy
            };
            _applicationDbContext.BookIssues.Add(newBookIssue);
            await _applicationDbContext.SaveChangesAsync();
            return new CreateBookIssueDto
            {
                BookId = newBookIssue.BookId,
                UserId = newBookIssue.UserId,
                IssueDate = newBookIssue.IssueDate,
                DueDate = newBookIssue.DueDate,
                ReturnDate = newBookIssue.ReturnDate,
                Status = newBookIssue.Status,
                FineAmount = newBookIssue.FineAmount,
                Remarks = newBookIssue.Remarks,
                IssuedBy = newBookIssue.IssuedBy
            };
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookIssue = await _applicationDbContext.BookIssues.FindAsync(id);
            if (bookIssue == null)
            {
                throw new KeyNotFoundException($"Book issue with ID {id} not found.");
            }
            _applicationDbContext.BookIssues.Remove(bookIssue);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<BookIssueDto>> GetBookIssueAsync(int pageNumber, int pageSize)
        {
            var bookIssues = await _applicationDbContext.BookIssues
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(bi => new BookIssueDto
                {
                    IssueId = bi.IssueId,
                    BookId = bi.BookId,
                    UserId = bi.UserId,
                    IssueDate = bi.IssueDate,
                    DueDate = bi.DueDate,
                    ReturnDate = bi.ReturnDate,
                    Status = bi.Status,
                    FineAmount = bi.FineAmount,
                    Remarks = bi.Remarks,
                    IssuedBy = bi.IssuedBy
                }).OrderByDescending(x => x.IssueId).ToListAsync();
            return bookIssues;
        }

        public async Task<BookIssueDto> GetByIdAsync(int id)
        {
            var bookIssue = await _applicationDbContext.BookIssues.FindAsync(id);
            return new BookIssueDto
            {
                IssueId = bookIssue.IssueId,
                BookId = bookIssue.BookId,
                UserId = bookIssue.UserId,
                IssueDate = bookIssue.IssueDate,
                DueDate = bookIssue.DueDate,
                ReturnDate = bookIssue.ReturnDate,
                Status = bookIssue.Status,
                FineAmount = bookIssue.FineAmount,
                Remarks = bookIssue.Remarks,
                IssuedBy = bookIssue.IssuedBy
            };
        }

        public async Task<BookIssueDto> UpdateBookAsync(int id, CreateBookIssueDto bookIssueDto)
        {
            var bookIssue = await _applicationDbContext.BookIssues.FindAsync(id);
            if (bookIssue == null)
            {
                throw new KeyNotFoundException($"Book issue with ID {id} not found.");
            }
            bookIssue.BookId = bookIssueDto.BookId;
            bookIssue.UserId = bookIssueDto.UserId;
            bookIssue.IssueDate = bookIssueDto.IssueDate;
            bookIssue.DueDate = bookIssueDto.DueDate;
            bookIssue.ReturnDate = bookIssueDto.ReturnDate;
            bookIssue.Status = bookIssueDto.Status;
            bookIssue.FineAmount = bookIssueDto.FineAmount;
            bookIssue.Remarks = bookIssueDto.Remarks;
            bookIssue.IssuedBy = bookIssueDto.IssuedBy;
            _applicationDbContext.BookIssues.Update(bookIssue);
            await _applicationDbContext.SaveChangesAsync();
            return new BookIssueDto
            {
                IssueId = bookIssue.IssueId,
                BookId = bookIssue.BookId,
                UserId = bookIssue.UserId,
                IssueDate = bookIssue.IssueDate,
                DueDate = bookIssue.DueDate,
                ReturnDate = bookIssue.ReturnDate,
                Status = bookIssue.Status,
                FineAmount = bookIssue.FineAmount,
                Remarks = bookIssue.Remarks,
                IssuedBy = bookIssue.IssuedBy
            };
        }
    }
}
