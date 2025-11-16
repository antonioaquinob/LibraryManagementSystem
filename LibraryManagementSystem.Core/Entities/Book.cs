namespace LibraryManagementSystem.Core.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int PublishedMonth { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
