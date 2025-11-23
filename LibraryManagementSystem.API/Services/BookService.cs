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

        public async Task<Book?> UpdateBookAsync(int id, UpdateBookDto dto)
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

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return false;

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }
    }
}
