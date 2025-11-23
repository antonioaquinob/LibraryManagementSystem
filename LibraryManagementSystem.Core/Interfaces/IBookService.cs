using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBookAsync(CreateBookDto dto);
        Task<Book?> UpdateBookAsync(int id, UpdateBookDto dto);
        Task<bool> DeleteBookAsync(int id);
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
    }
}
