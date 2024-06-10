namespace Biblioteca.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string genre { get; set; }
        public int YearPub { get; set; }
        public List<User> Users { get; } = [];
        public List<Loan> Loans { get; } = [];
    }
}
