using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.DTOs;
namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int bookId);
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        Task<BookDto?> UpdateBookAsync(int bookId, CreateBookDto dto);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
