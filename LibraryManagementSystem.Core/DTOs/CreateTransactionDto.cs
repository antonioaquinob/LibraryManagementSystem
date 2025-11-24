using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.DTOs
{
    public class CreateTransactionDto
    {
        public int BookId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
    }
    public class ReturnTransactionDto
    {
        public int TransactionId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
