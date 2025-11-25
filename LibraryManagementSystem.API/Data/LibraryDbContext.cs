using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.API.Data
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookTransaction> BookTransactions { get; set; } = null!;
        public DbSet<User> Users { get; set; }

    }
}
