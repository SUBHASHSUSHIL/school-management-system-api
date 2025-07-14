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
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateBookDto> CreateBookAsync(CreateBookDto book)
        {
            var newbook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Edition = book.Edition,
                CategoryId = book.BookCategoryId,
                Price = book.Price,
                Quantity = book.Quantity,
                AvailableQuantity = book.AvailableQuantity,
                ShelfNumber = book.ShelfNumber
            };
            _applicationDbContext.Books.Add(newbook);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _applicationDbContext.Books
                .FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            _applicationDbContext.Books.Remove(book);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _applicationDbContext.Books
                .FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            var bookDto = new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Edition = book.Edition,
                BookCategoryId = book.CategoryId,
                Price = book.Price,
                Quantity = book.Quantity,
                AvailableQuantity = book.AvailableQuantity,
                ShelfNumber = book.ShelfNumber
            };
            return bookDto;
        }

        public async Task<List<BookDto>> GetBooksAsync(int pageNumber, int pageSize)
        {
            var books = await _applicationDbContext.Books.OrderByDescending(b => b.BookId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Publisher = b.Publisher,
                    Edition = b.Edition,
                    BookCategoryId = b.CategoryId,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    AvailableQuantity = b.AvailableQuantity,
                    ShelfNumber = b.ShelfNumber
                }).OrderByDescending(x => x.BookId).ToListAsync();
            return books;
        }

        public async Task<CreateBookDto> UpdateBookAsync(int id, CreateBookDto book)
        {
            var existingBook = await _applicationDbContext.Books
                .FirstOrDefaultAsync(b => b.BookId == id);
            if (existingBook == null)
                {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Publisher = book.Publisher;
            existingBook.Edition = book.Edition;
            existingBook.CategoryId = book.BookCategoryId;
            existingBook.Price = book.Price;
            existingBook.Quantity = book.Quantity;
            existingBook.AvailableQuantity = book.AvailableQuantity;
            existingBook.ShelfNumber = book.ShelfNumber;
            _applicationDbContext.Books.Update(existingBook);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }
    }
}
