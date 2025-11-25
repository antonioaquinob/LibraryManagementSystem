using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookTransaction> BookTransactions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly define primary keys
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookTransaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            // Optional: configure relationship
            modelBuilder.Entity<BookTransaction>()
                .HasOne(t => t.Book)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
