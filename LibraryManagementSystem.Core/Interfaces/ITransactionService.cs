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
        Task<TransactionResponseDto> BorrowBookAsync(CreateTransactionDto dto);
        Task<TransactionResponseDto?> ReturnBookAsync(ReturnTransactionDto dto);
        Task<BookTransaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<BookTransaction>> GetAllTransactionsAsync();
    }
}
