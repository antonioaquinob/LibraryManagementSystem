using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.API.Data;
using LibraryManagementSystem.Core.DTOs;

namespace LibraryManagementSystem.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public Task<Book> AddAsync(CreateBookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Category = bookDto.Category,
                ISBN = bookDto.ISBN,
                PublishDate = bookDto.PublishDate,
                QuantityAvailable = bookDto.QuantityAvailable
            };

            _context.Books.Add(book);
            return Task.FromResult(book);
        }

        public Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            return Task.FromResult(book);
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            var findBook = await _context.Books.FindAsync(book.BookId);
            if (findBook == null)
                return false;

            _context.Books.Remove(findBook);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
