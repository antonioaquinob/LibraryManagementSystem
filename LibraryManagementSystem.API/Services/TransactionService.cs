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

    // Borrow book
    public async Task<TransactionResponseDto> BorrowBookAsync(CreateTransactionDto dto)
    {
        var book = await _bookRepo.GetBookByIdAsync(dto.BookId);
        if (book == null || book.QuantityAvailable <= 0)
            throw new Exception("Book not available");

        // Decrease available quantity
        book.QuantityAvailable--;
        await _bookRepo.UpdateAsync(book);
        await _bookRepo.SaveChangesAsync();

        var transaction = new BookTransaction
        {
            BookId = dto.BookId,
            BorrowerName = dto.BorrowerName,
            BorrowDate = dto.BorrowDate
        };

        var savedTransaction = await _repo.BorrowBookAsync(transaction);

        return new TransactionResponseDto
        {
            TransactionId = savedTransaction.TransactionId,
            BookId = savedTransaction.BookId,
            BookTitle = book.Title,
            BorrowerName = savedTransaction.BorrowerName,
            BorrowDate = savedTransaction.BorrowDate,
            ReturnDate = savedTransaction.ReturnDate,
            Penalty = savedTransaction.Penalty
        };
    }

    // Return book
    public async Task<TransactionResponseDto?> ReturnBookAsync(ReturnTransactionDto dto)
    {
        var transaction = await _repo.GetTransactionByIdAsync(dto.TransactionId);
        if (transaction == null) return null;

        transaction.ReturnDate = dto.ReturnDate;

        // Calculate penalty if overdue (2 weeks)
        if (transaction.BorrowDate.AddDays(14) < dto.ReturnDate)
        {
            transaction.Penalty = 10; // example value
        }

        // Update book quantity
        var book = await _bookRepo.GetBookByIdAsync(transaction.BookId);
        if (book != null)
        {
            book.QuantityAvailable++;
            await _bookRepo.UpdateAsync(book);
        }

        // Save transaction
        await _repo.UpdateTransactionAsync(transaction);
        await _repo.SaveChangesAsync();

        return new TransactionResponseDto
        {
            TransactionId = transaction.TransactionId,
            BookId = transaction.BookId,
            BookTitle = book?.Title ?? "",
            BorrowerName = transaction.BorrowerName,
            BorrowDate = transaction.BorrowDate,
            ReturnDate = transaction.ReturnDate,
            Penalty = transaction.Penalty
        };
    }

    public async Task<BookTransaction?> GetTransactionByIdAsync(int id)
    {
        return await _repo.GetTransactionByIdAsync(id);
    }

    public async Task<IEnumerable<BookTransaction>> GetAllTransactionsAsync()
    {
        return await _repo.GetAllAsync();
    }
}
