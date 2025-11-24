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
        Task<IEnumerable<BookTransaction>> GetAllAsync();
        Task<BookTransaction?> GetByIdAsync(int id);
        Task<BookTransaction> BorrowBookAsync(BookTransaction transaction);
        Task<BookTransaction?> ReturnBookAsync(int transactionId, DateTime returnDate);
        Task SaveChangesAsync();
    }
}
