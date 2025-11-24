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

        public async Task<BookTransaction?> GetByIdAsync(int id)
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

        public async Task<BookTransaction?> ReturnBookAsync(int transactionId, DateTime returnDate)
        {
            var transaction = await _context.BookTransactions
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);

            if (transaction == null)
                return null;

            transaction.ReturnDate = returnDate;

            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
