using LibraryManagementSystem.API.Data;
using LibraryManagementSystem.API.Repositories;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        private readonly IBookRepository _bookRepository;
        public BookService(LibraryDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateBookAsync(CreateBookDto dto)
        {
            try
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

                await _bookRepository.AddAsync(dto);
                await _bookRepository.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw; // Let the controller handle the error
            }
        }

        public async Task<Book?> UpdateBookAsync(int id, UpdateBookDto dto)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                if (book == null)
                    return null;

                // Update fields using BookDto
                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Category = dto.Category;
                book.ISBN = dto.ISBN;
                book.PublishDate = dto.PublishDate;
                book.QuantityAvailable = dto.QuantityAvailable;

                await _bookRepository.UpdateAsync(book);
                await _bookRepository.SaveChangesAsync();
                return book;
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw; // Let the controller handle the error
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                if (book == null)
                    return false;

                await _bookRepository.DeleteAsync(book);
                await _bookRepository.SaveChangesAsync();
                return true;

            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw; // Let the controller handle the error
            }
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? search = null, string? sortBy = null, bool sortDesc = false)
        {
            IQueryable<Book> query = _context.Books;

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(search) ||
                    b.Author.ToLower().Contains(search) ||
                    b.Category.ToLower().Contains(search));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "title" => sortDesc ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title),
                    "author" => sortDesc ? query.OrderByDescending(b => b.Author) : query.OrderBy(b => b.Author),
                    "publishdate" => sortDesc ? query.OrderByDescending(b => b.PublishDate) : query.OrderBy(b => b.PublishDate),
                    _ => query
                };
            }

            return await query.ToListAsync();
        }
    }
}
