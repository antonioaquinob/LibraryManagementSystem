using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Entities
{
    public class BookTransaction
    {
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable for not-yet-returned books
        public decimal? Penalty { get; set; } // Calculated when returned

        public Book? Book { get; set; } // Navigation property
    }
}
