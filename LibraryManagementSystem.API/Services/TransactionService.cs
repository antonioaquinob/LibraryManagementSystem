using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;
    private readonly IBookRepository _bookRepo;

    public TransactionService(ITransactionRepository repo, IBookRepository bookRepo)
    {
        _repo = repo;
        _bookRepo = bookRepo;
    }

    public async Task<BookTransaction> BorrowBookAsync(CreateTransactionDto dto)
    {
        var book = await _bookRepo.GetBookByIdAsync(dto.BookId);
        if (book == null || book.QuantityAvailable <= 0)
            throw new Exception("Book not available");

        book.QuantityAvailable--;
        await _bookRepo.UpdateAsync(book);
        await _bookRepo.SaveChangesAsync();

        var transaction = new BookTransaction
        {
            BookId = dto.BookId,
            BorrowerName = dto.BorrowerName,
            BorrowDate = dto.BorrowDate
        };

        return await _repo.BorrowBookAsync(transaction);
    }

    public async Task<BookTransaction?> ReturnBookAsync(ReturnTransactionDto dto)
    {
        var transaction = await _repo.ReturnBookAsync(dto.TransactionId, dto.ReturnDate);
        if (transaction == null)
            return null;

        var book = await _bookRepo.GetBookByIdAsync(transaction.BookId);
        if (book != null)
        {
            book.QuantityAvailable++;
            await _bookRepo.UpdateAsync(book);
            await _bookRepo.SaveChangesAsync();
        }

        var dueDate = transaction.BorrowDate.AddDays(7);
        if (dto.ReturnDate > dueDate)
            transaction.Penalty = (dto.ReturnDate - dueDate).Days * 10;

        await _repo.SaveChangesAsync();
        return transaction;
    }

    public async Task<BookTransaction?> GetTransactionByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<BookTransaction>> GetAllTransactionsAsync()
    {
        return await _repo.GetAllAsync();
    }
}
