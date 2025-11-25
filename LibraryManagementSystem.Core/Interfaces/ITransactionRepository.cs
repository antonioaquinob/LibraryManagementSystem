using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<BookTransaction?> GetTransactionByIdAsync(int id);
        Task<BookTransaction> BorrowBookAsync(BookTransaction transaction);
        Task UpdateTransactionAsync(BookTransaction transaction);
        Task SaveChangesAsync();
        Task<IEnumerable<BookTransaction>> GetAllAsync();
    }
}
