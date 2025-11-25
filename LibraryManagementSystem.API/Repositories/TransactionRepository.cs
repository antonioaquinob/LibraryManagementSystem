using LibraryManagementSystem.API.Data;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LibraryDbContext _context;

        public TransactionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookTransaction>> GetAllAsync()
        {
            return await _context.BookTransactions.ToListAsync();
        }

        public async Task<BookTransaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.BookTransactions
                .FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task<BookTransaction> BorrowBookAsync(BookTransaction transaction)
        {
            await _context.BookTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task UpdateTransactionAsync(BookTransaction transaction)
        {
            _context.BookTransactions.Update(transaction);
            await Task.CompletedTask; // EF tracks changes, update is optional
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
