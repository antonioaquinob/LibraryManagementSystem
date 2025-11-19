using LibraryManagementSystem.API.Data;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateBookAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Category = dto.Category,
                ISBN = dto.ISBN,
                PublishDate = dto.PublishDate,
                QuantityAvailable = dto.QuantityAvailable
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBookAsync(int id, BookDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return null;

            // Update fields using BookDto
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Category = dto.Category;
            book.ISBN = dto.ISBN;
            book.PublishDate = dto.PublishDate;
            book.QuantityAvailable = dto.QuantityAvailable;

            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
    }
}
