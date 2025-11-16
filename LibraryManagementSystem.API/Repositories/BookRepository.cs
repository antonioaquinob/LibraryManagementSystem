using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.API.Data;
using System.Runtime.InteropServices;
namespace LibraryManagementSystem.API.Repositories
{
    public class BookRepository: IBookRepository
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
        public async Task<Book?> GetByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }
        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<bool> DeleteAsync(Book book)
        {
            var findBook = await _context.Books.FindAsync(book.BookId);
            if (findBook != null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
