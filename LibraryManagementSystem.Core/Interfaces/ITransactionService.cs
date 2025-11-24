using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<BookTransaction> BorrowBookAsync(CreateTransactionDto dto);
        Task<BookTransaction?> ReturnBookAsync(ReturnTransactionDto dto);
        Task<IEnumerable<BookTransaction>> GetAllTransactionsAsync();
        Task<BookTransaction?> GetTransactionByIdAsync(int transactionId);
    }
}
