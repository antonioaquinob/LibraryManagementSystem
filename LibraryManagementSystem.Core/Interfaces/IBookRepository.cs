using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetBookByIdAsync(int bookId);
        Task<Book> AddAsync(CreateBookDto book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(Book book);
        Task SaveChangesAsync();
    }
}
